using flashcard_backend.Models;

namespace flashcard_backend.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public UserRole Role { get; set; }
    // public UserStatus Status { get; set; }

}