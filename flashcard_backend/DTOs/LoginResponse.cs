namespace flashcard_backend.DTOs;

public class LoginResponse
{
    public string? Email { get; set; }
    public string? AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
}