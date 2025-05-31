namespace flashcard_backend.DTOs;

public class TopicResponseDto
{
    public int TopicId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}