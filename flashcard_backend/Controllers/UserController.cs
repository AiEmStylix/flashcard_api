using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

    [HttpDelete("bulk")]
    public async Task<IActionResult> DeleteMultipleUserAsync([FromBody] List<int> uIds)
    {
        if (uIds == null | uIds.Count == 0)
        {
            return BadRequest(new { message = "No User Id provided" });
        }

        var result = await _userService.DeleteMultipleUsersAsync(uIds);

        return Ok(new
        {
            deleted = result.Deleted,
            notFound = result.NotFound
        });
    }

}