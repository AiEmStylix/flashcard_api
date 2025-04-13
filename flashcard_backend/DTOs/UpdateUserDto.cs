using System.ComponentModel.DataAnnotations;
using flashcard_backend.Models;

namespace flashcard_backend.DTOs;

public class UpdateUserDto
{
    public string? Email { get; set; }
    [StringLength(100)]
    public string? FullName { get; set; }

    public UserRole? Role { get; set; }

    // public UserStatus? Status { get; set; }
}