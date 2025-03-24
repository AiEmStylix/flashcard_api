using System.ComponentModel.DataAnnotations;
using flashcard_backend.Models;

namespace flashcard_backend.DTOs;

public class CreateUserDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; }

    [Required]
    public UserRole Role { get; set; }
}