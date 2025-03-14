using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;
using UsersAPI.Repositories;

namespace UsersAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _repository;

    public UsersController(UserRepository userRepository)
    {
        _repository = userRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _repository.GetAll();
        return Ok(users);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _repository.GetById(id);
        return user != null ? Ok(user) : NotFound();
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        var existingUser = await _repository.GetByEmail(userDto.Email);
        if (existingUser != null)
        {
            return BadRequest("Email is already in use.");
        }

        var user = userDto.ToUser();

        await _repository.Add(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
    {
        var user = userDto.ToUserWithId(id);
        Console.Write(user);

        await _repository.Update(user);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingUser = await _repository.GetById(id);
        if (existingUser == null)
            return NotFound();

        await _repository.Delete(id);
        return NoContent();
    }
}
