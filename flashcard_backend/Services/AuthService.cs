using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;

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

    public async Task<bool> ValidateUser(LoginRequestDto loginDto)
    {
        var user = await _userRepository.GetUserByUserName(loginDto.Username);
        if (user == null)
        {
            return false;
        }
        return _passwordService.VerifyPassword(user.PasswordHash, loginDto.Password);
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
}