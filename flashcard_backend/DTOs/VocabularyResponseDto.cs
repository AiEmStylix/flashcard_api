namespace flashcard_backend.DTOs;

public class VocabularyResponseDto
{
    public int VocabularyId { get; set; }
    public string Word { get; set; }
    public string Definition { get; set; }
    public int TopicId { get; set; }
    public DateTime CreatedAt { get; set; }
}