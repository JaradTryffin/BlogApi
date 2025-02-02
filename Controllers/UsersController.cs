using BlogApplication.DTO;
using BlogApplication.Models;
using BlogApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController:ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(UserCreateDto userDto)
    {
        var user = new User
        {
            Username = userDto.Username,
            Email = userDto.Email
            // Posts will be initialized as an empty list in the repository
        };
    
        var createdUser = await _userRepository.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }
        await _userRepository.UpdateUserAsync(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userRepository.DeleteUserAsync(id);
        return NoContent();
    }
}