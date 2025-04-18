using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;

namespace flashcard_backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    public UserService(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
            // Status = user.Status
        }).ToList();
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
        };
    }
    

    public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto userDto)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(id);
        if (existingUser == null)
        {
            return null;
        }
        // Manually update only the properties that are allowed to be changed
        if (!string.IsNullOrWhiteSpace(userDto.Email))
        {
            existingUser.Email = userDto.Email;
        }
        if (!string.IsNullOrWhiteSpace(userDto.FullName))
            existingUser.FullName = userDto.FullName;

        if (userDto.Role.HasValue)
            existingUser.Role = userDto.Role.Value;

        var updatedUser = await _userRepository.UpdateUserAsync(existingUser);

        // Manual mapping back to DTO
        return new UserDto
        {
            Id = updatedUser.Id,
            Username = updatedUser.Username,
            Email = updatedUser.Email,
            FullName = updatedUser.FullName,
            Role = updatedUser.Role,
        };
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }
    
    public async Task<DeleteUserResultDto> DeleteMultipleUsersAsync(List<int> userIds)
    {
        return await _userRepository.DeleteMutipleUsersAsync(userIds);
    }
}