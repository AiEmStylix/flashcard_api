using System.ComponentModel.DataAnnotations;

namespace flashcard_backend.DTOs;

public class VocabularyCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Word { get; set; }

    [Required]
    public string Definition { get; set; }

    public int TopicId { get; set; }
}