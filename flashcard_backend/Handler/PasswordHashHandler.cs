namespace flashcard_backend.Handler;

public class PasswordHashHandler
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
    }

    public static bool verifyPassword(string hashedPassword, string providedPassword)
    {
        var isValid = BCrypt.Net.BCrypt.EnhancedVerify(providedPassword, hashedPassword);
        return isValid;
    }
}