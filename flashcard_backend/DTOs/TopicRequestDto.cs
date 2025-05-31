namespace flashcard_backend.DTOs;

public class TopicRequestDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
}