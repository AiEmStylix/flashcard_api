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
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.Authenticate(request);
        if (result is null)
        {
            return Unauthorized();
        }

        return result;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.Register(request);
        if (result is null)
        {
            return Unauthorized();
        }

        return result;
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<ActionResult<LoginResponse?>> Refresh([FromBody] RefreshRequest request)
    {
        if (string.IsNullOrEmpty(request.Token))
        {
            return BadRequest("Invalid token");
        }

        var result = await _authService.RefreshToken(request.Token);
        return result is not null ? result : Unauthorized();
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

    private void SetRefreshTokenCookies(string refreshtoken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7),
            Secure = true,
            SameSite = SameSiteMode.Strict
        };
        Response.Cookies.Append("refreshToken", refreshtoken, cookieOptions);
    }
}