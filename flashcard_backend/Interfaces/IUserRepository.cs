using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IUserRepository
{
    Task AddUserAsync(UserModel user);
    Task<UserModel?> GetByEmailAsync(string email);
    Task<UserModel?> GetByUsernameAsync(string username);
}