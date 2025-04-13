using flashcard_backend.DTOs;
using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IAuthService
{
    Task<bool> ValidateUser(LoginRequestDto loginDto);
    Task<bool> RegisterUser(CreateUserDto createUserDto);
}