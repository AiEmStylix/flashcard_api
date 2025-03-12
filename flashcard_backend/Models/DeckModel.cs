using System.ComponentModel.DataAnnotations;

namespace flashcard_backend.Models;

public class DeckModel
{
    public string DeckId { get; set; }
    public int UserId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CategoryId { get; set; }
    
    //Setup relationship
    //Using virtual keyword for lazy-loading
    public virtual List<CardModel> Cards { get; set; } = new();
}