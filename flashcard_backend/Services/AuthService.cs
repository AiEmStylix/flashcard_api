using System.Security.Claims;
using System.Security.Cryptography;
using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace flashcard_backend.Services;

public class AuthService : IAuthService
{
    private readonly IPasswordService _passwordService;
    private readonly IUserRepository _userRepository;

    public AuthService(IPasswordService passwordService, IUserRepository userRepository)
    {
        _passwordService = passwordService;
        _userRepository = userRepository;
    }

    public async Task<(bool success, string message, UserModel user)> ValidateUser(LoginRequestDto loginDto)
    {
        var user = await _userRepository.GetUserByEmail(loginDto.Email);
        
        if (user == null)
        {
            return (false, "Invalid email or password", null);
        }

        if (!_passwordService.VerifyPassword(user.PasswordHash, loginDto.Password))
        {
            return (false, "Invalid email or password", null);
        };
        return (true, "Authentication successful", user);
    }

    public async Task<bool> RegisterUser(CreateUserDto createUserDto)
    {
        var userByUsername = await _userRepository.GetUserByUserName(createUserDto.Username);
        var userByEmail = await _userRepository.GetUserByEmail(createUserDto.Email);
        if (userByEmail != null || userByEmail != null)
        {
            return false;
        }
        var hashPassword= _passwordService.HashPassword(createUserDto.Password);
        var newUser = new UserModel
        {
            Username = createUserDto.Username,
            PasswordHash = hashPassword,
            FullName = createUserDto.FullName,
            Email = createUserDto.Email,
            Role = UserRole.User,
            CreatedAt = DateTime.UtcNow
        };
        await _userRepository.CreateUserAsync(newUser);
        return true;
    }

    public async Task SignInUser(HttpContext httpContext, UserModel user, bool rememberme)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FullName ?? string.Empty)
        };
        var claimIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = rememberme,
            ExpiresUtc = rememberme
                ? DateTimeOffset.UtcNow.AddDays(7)
                : DateTimeOffset.UtcNow.AddHours(1),
            AllowRefresh = true
        };

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimIdentity), authProperties);
        //If remember me is checked
        if (rememberme)
        {
            string persistentToken = GeneratePersistentToken();

            user.PersistentSessionToken = persistentToken;
            user.PersistentSessionExpiry = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateUserAsync(user);
            
            //Set persistent cookie
            httpContext.Response.Cookies.Append(
                "PersistentToken",
                persistentToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.Now.AddDays(7)
                });
        }

        user.LastLogin = DateTime.UtcNow;
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task SignOutUser(HttpContext httpContext)
    {
        if (httpContext.User.Identity.IsAuthenticated)
        {
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user != null)
                {
                    //Clear token
                    user.PersistentSessionToken = null;
                    user.PersistentSessionExpiry = null;
                    await _userRepository.UpdateUserAsync(user);
                }
            }
        }
        
        //Delete token cookie and sign out
        httpContext.Response.Cookies.Delete("PersistentToken");
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<UserModel> GetUserByPersistentToken(string token)
    {
        return await _userRepository.GetUserByPersistentToken(token);
    }

    public string GeneratePersistentToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}