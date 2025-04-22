using System.Security.Claims;
using System.Security.Cryptography;
using flashcard_backend.DTOs;
using flashcard_backend.Handler;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace flashcard_backend.Services;

public class AuthService : IAuthService
{
    private IUserRepository _userRepository;
    private IRefreshTokenRepository _refreshTokenRepository;
    private IJwtService _jwtService;
    private IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IJwtService jwtService, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtService = jwtService;
        _configuration = configuration;
    }
    
    public async Task<LoginResponse?> RefreshToken(string token)
    {
        var refreshToken = await _refreshTokenRepository.GetRefreshToken(token);
        if (refreshToken is null | refreshToken.Expiry < DateTime.UtcNow)
        {
            return null;
        }
        
        await _refreshTokenRepository.DeleteRefreshToken(refreshToken);

        var user = await _userRepository.GetUserByIdAsync(refreshToken.UserId);

        if (user is null) return null;
        
        var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

        return new LoginResponse
        {
            AccessToken = _jwtService.GenerateAccessToken(user.Email),
            Email = user.Email,
            RefreshToken = _jwtService.GenerateRefreshToken(),
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
        };
    }

    public async Task<LoginResponse?> Authenticate(LoginRequestDto request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return null;
        }
    
        var userAccount = await _userRepository.GetUserByEmail(request.Email);
        if (userAccount is null || !PasswordHashHandler.verifyPassword(userAccount.PasswordHash, request.Password))
        {
            Console.WriteLine("User not found");
            return null;
        }
        
        var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);
        var accessToken = _jwtService.GenerateAccessToken(request.Email);
        var refreshToken = _jwtService.GenerateRefreshToken();

        await _refreshTokenRepository.CreateRefreshToken(new RefreshToken
        {
            Token = refreshToken,
            UserId = userAccount.Id,
            Expiry = tokenExpiryTimeStamp
        });
        
        return new LoginResponse
        {
            AccessToken = accessToken,
            Email = request.Email,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
            RefreshToken = refreshToken,
        };
    }

    public async Task<RegisterResponse?> Register(RegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Username))
        {
            return null;
        }
        
        //Check if account exists in database
        var userAccount = await _userRepository.GetUserByEmail(request.Email);
        if (userAccount is not null)
        {
            return null;
        }

        var registerAccount = new UserModel
        {
            Email = request.Email,
            Username = request.Username,
            FullName = request.FullName,
            PasswordHash = PasswordHashHandler.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow,
            Role = UserRole.User
        };
        
        //Save user in the database
        var createdUser = await _userRepository.CreateUserAsync(registerAccount);

        var userId = createdUser.Id;
        
        var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);
        var accessToken = _jwtService.GenerateAccessToken(request.Email);
        var refreshToken = _jwtService.GenerateRefreshToken();
        
        //Save the refresh token in the database
        await _refreshTokenRepository.CreateRefreshToken(new RefreshToken
        {
            Token = refreshToken,
            UserId = userId,
            Expiry = tokenExpiryTimeStamp
        });

        return new RegisterResponse
        {
            Email = request.Email,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
        };
    }
}