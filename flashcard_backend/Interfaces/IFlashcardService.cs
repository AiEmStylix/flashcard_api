using flashcard_backend.DTOs;
using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IFlashcardService
{
    Task<IEnumerable<FlashcardResponse?>> GetAllFlashcard();
    Task<CardModel?> GetFlashcardById (int id);
}