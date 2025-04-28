using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flashcard_backend.Models;

[Table("flashcards")]
public class CardModel
{
    [Key]
    public int Id { get; set; }

    public string Question { get; set; }
    public string Answer { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastReviewed { get; set; }
    public int DeckId { get; set; }
    
    // Navigation property
    public virtual DeckModel Deck { get; set; }
}
