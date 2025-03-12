namespace flashcard_backend.Models;

public class CardModel
{
    public int CardId { get; set; }
    public int DeckId { get; set; }
    public string FrontContent { get; set; }
    public string BackContent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Ordering { get; set; }
}
