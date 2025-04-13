using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;

namespace flashcard_backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
            // Status = user.Status
        };
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
    {
        var user = new UserModel
        {
            Username = userDto.Username,
            Email = userDto.Email,
            FullName = userDto.FullName,
            //Default password 
            PasswordHash = "1234",
            Role = userDto.Role,
            // Status = UserStatus.Active,
            CreatedAt = DateTime.UtcNow
        };
        var createdUser = await _userRepository.CreateUserAsync(user);
        if (createdUser == null)
        {
            return null;
        }
        return new UserDto
        {
            Id = createdUser.Id,
            Username = createdUser.Username,
            Email = createdUser.Email,
            FullName = createdUser.FullName,
            Role = createdUser.Role,
            // Status = createdUser.Status
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

        // if (userDto.Status.HasValue)
        //     existingUser.Status = userDto.Status.Value;

        var updatedUser = await _userRepository.UpdateUserAsync(id, existingUser);

        // Manual mapping back to DTO
        return new UserDto
        {
            Id = updatedUser.Id,
            Username = updatedUser.Username,
            Email = updatedUser.Email,
            FullName = updatedUser.FullName,
            Role = updatedUser.Role,
            // Status = updatedUser.Status
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