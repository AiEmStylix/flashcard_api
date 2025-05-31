using System.ComponentModel.DataAnnotations;
using flashcard_backend.Models;

namespace flashcard_backend.DTOs;

public class UpdateUserDto
{
    public string? Email { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public UserRole? Role { get; set; }

    // public UserStatus? Status { get; set; }
}