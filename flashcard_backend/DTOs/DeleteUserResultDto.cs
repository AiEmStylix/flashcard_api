namespace flashcard_backend.DTOs;

public class DeleteUserResultDto
{
    public List<int> Deleted { get; set; }
    public List<int> NotFound { get; set; }
}