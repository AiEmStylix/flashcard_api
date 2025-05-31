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
    
    //Foregin key
    public int TopicId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    //Navigation Props
    [ForeignKey("TopicId")]
    public virtual Topic Topic { get; set; }
    
}