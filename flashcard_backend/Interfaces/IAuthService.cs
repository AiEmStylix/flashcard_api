using flashcard_backend.DTOs;
using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> Authenticate(LoginRequestDto request);
    Task<RegisterResponse?> Register(RegisterRequest request);
    Task<LoginResponse?> RefreshToken(string token);
}