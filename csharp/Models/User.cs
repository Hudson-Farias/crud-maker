using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Models;


public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}



public class UserDto
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; } = string.Empty;


    public User ToUser()
    {   
        return new User
        {
            Name = this.Name,
            Email = this.Email
        };
    }


    public User ToUserWithId(int id)
    {   
        return new User
        {
            Id = id,
            Name = this.Name,
            Email = this.Email
        };
    }
}