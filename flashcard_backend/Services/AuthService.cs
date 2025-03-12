using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;

namespace flashcard_backend.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> ValidateUser(LoginRequestDto loginDto)
    {
        var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
        return user != null && user.PasswordHash == loginDto.Password;
    }

    public async Task<UserModel> GetUserByUsernameAsync(string username)
    {
        return await _userRepository.GetByUsernameAsync(username);
    }
}