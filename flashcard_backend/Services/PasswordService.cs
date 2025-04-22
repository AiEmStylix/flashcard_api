using flashcard_backend.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace flashcard_backend.Services;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        return isValid;
    }
}