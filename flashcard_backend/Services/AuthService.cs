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
    private IUserRepository _userRepository;
    private IRefreshTokenRepository _refreshTokenRepository;
    private IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtService = jwtService;
    }

    
}