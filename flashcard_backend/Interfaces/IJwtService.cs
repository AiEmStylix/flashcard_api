using flashcard_backend.DTOs;
using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(string email);
    string GenerateRefreshToken();
}