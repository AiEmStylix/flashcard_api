using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flashcard_backend.Models;

[Table("decks")]
public class DeckModel
{
    [Key] 
    public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }

    //Navigation properties
    public virtual UserModel User { get; set; }
    public virtual ICollection<CardModel> FlashCards { get; set; }
}