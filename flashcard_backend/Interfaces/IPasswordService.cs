namespace flashcard_backend.Interfaces;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string providedPassword);
}