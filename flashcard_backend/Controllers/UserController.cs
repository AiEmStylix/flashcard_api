using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace flashcard_backend.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
    {
        if (userDto == null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedUser = await _userService.UpdateUserAsync(id, userDto);
        if (updatedUser == null)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var deleteUser = await _userService.DeleteUserAsync(id);
        if (deleteUser == null)
        {
            return NotFound();
        }

        return Ok(deleteUser);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewUserAsync([FromBody] CreateUserDto newUserDto) {
        var newUser = await _userService.CreateUserAsync(newUserDto);
        if (newUser == null)
        {
            return BadRequest();
        }
        return Ok(newUser);
    }
}