using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flashcard_backend.Models;

public class Vocabulary
{
    [Key]
    public int VocabularyId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Word { get; set; }
    
    [Required]
    public string Definition { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    //Navigation Property
    public int TopicId { get; set; }
    public  Topic Topic { get; set; }
    
}