namespace flashcard_backend.DTOs;

public class FlashcardResponse
{
    
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public DateTime CreatedAt { get; set; }
    public int DeckId { get; set; }
}