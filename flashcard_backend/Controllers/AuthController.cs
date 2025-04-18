using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace flashcard_backend.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var (success, message, user) = await _authService.ValidateUser(loginDto);
        if (!success)
        {
            return Unauthorized(new LoginResponse
            {
                Success = false,
                Message = message
            });
        }

        await _authService.SignInUser(HttpContext, user, loginDto.RememberMe);
        return Ok(new LoginResponse
        {
            Success = true,
            Message = "Login successful",
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role
            }
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto registerDto)
    {
        var result = await _authService.RegisterUser(registerDto);
        if (!result)
        {
            return BadRequest(new { message = "User/Email already exists" });
        }

        return Ok(new
        {
            Username = registerDto.Username,
            Message = "Register successfully",

        });
    }

    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var username = HttpContext.Session.GetString("Username");
        if (username == null)
        {
            return Unauthorized(new { message = "Not logged in" });
        }

        return Ok(new { username });
    }

    [HttpPost("logout")]
    public IActionResult LogOut()
    {
        HttpContext.Session.Clear();
        return Ok(new { message = "Logout succesfully" });
    }
}