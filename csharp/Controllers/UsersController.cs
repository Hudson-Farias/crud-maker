using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;
using UsersAPI.Repositories;

namespace UsersAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UsersController(IUserRepository userRepository)
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
    public async Task<IActionResult> Create([FromBody] User user)
    {
        await _repository.Add(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] User user)
    {
        var existingUser = await _repository.GetById(id);
        if (existingUser == null)
            return NotFound();

        user.Id = id;
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
