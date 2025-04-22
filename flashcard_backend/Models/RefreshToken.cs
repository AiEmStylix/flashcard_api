using System.ComponentModel.DataAnnotations;

namespace flashcard_backend.Models;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime Expiry { get; set; }
    public int UserId { get; set; }
}