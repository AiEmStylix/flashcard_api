using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IFlashcardRepository
{
    Task<IEnumerable<CardModel>> GetAllFlashcardAsync();
    Task<CardModel> GetFlashcardByIdAsync(int id);
}