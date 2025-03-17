using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flashcard_backend.Models;

[Table("flashcards")]
public class CardModel
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Deck")]
    public int DeckId { get; set; }
    public DeckModel Deck { get; set; }

    [Required]
    public string Front { get; set; } = string.Empty;

    [Required]
    public string Back { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
