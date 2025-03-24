using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace flashcard_backend.Models;

[Table("users")]
public class UserModel
{   
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; }
    
    [Required]
    [StringLength(100)]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }
    
    public UserStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }

    // Relationships
    public List<DeckModel> Decks { get; set; } = new();
}

public enum UserRole
{
    User = 1,
    Admin = 2,
    Moderator = 3
}

public enum UserStatus
{
    Active = 1,
    Inactive = 2,
    Suspended = 3
}