namespace flashcard_backend.DTOs;

public class FlashcardRequest
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public int DeckId { get; set; }
}