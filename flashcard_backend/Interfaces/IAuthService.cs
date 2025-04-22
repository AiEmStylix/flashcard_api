using flashcard_backend.DTOs;
using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IAuthService
{
    Task<(bool success, string message, UserModel user)> ValidateUser(LoginRequestDto loginDto);
    Task<bool> RegisterUser(CreateUserDto createUserDto);
    Task SignInUser(HttpContext httpContext, UserModel user, bool rememberme);
    Task SignOutUser(HttpContext httpContext);
}