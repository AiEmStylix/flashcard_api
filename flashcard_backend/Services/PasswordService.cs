using flashcard_backend.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace flashcard_backend.Services;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<String> _hasher = new PasswordHasher<string>();

    public string HashPassword(string password)
    {
        return _hasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassoword)
    {
        var result = _hasher.VerifyHashedPassword(null, hashedPassword, providedPassoword);
        return result == PasswordVerificationResult.Success;
    }
}