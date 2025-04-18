using flashcard_backend.DTOs;

namespace flashcard_backend.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> UpdateUserAsync(int id, UpdateUserDto userDto);
    Task<bool> DeleteUserAsync(int id);
    Task<DeleteUserResultDto> DeleteMultipleUsersAsync(List<int> userIds);

}