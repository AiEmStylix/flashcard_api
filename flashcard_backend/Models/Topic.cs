using System.ComponentModel.DataAnnotations;

namespace flashcard_backend.Models;

public class Topic
{
    [Key]
    public int TopicId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; }
    
}