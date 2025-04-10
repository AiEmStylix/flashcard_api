using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
    Task<UserModel> GetUserByIdAsync(int id);
    Task<UserModel> CreateUserAsync(UserModel user);
    Task<UserModel> UpdateUserAsync(int id, UserModel user);
    Task<bool> DeleteUserAsync(int id);
}