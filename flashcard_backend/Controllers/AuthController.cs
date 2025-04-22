using System.Security.Claims;
using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace flashcard_backend.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequestDto request)
    {
        var result = await _jwtService.Authenticate(request);
        if (result is null)
        {
            return Unauthorized();
        }

        return result;
    }

    [Authorize]
    [HttpPost("me")]
    public IActionResult GetMe()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized(new { message = "User not found" });
        }

        return Ok(new { email });
    }
}