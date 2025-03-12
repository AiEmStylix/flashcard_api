using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace flashcard_backend.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task< IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var isValid = await _authService.ValidateUser(loginDto);
        if (!isValid)
        {
            return Unauthorized(new { message = "Invalid Credentials" });
        }

        var user = await _authService.GetUserByUsernameAsync(loginDto.Username);
        return Ok(new
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Message = "Login Succesfully"
        });
    }
}