using flashcard_backend.DTOs;
using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IFlashcardService
{
    Task<IEnumerable<FlashcardResponse?>> GetAllFlashcard();
    Task<FlashcardResponse?> GetFlashcardById (int id);
    Task<IEnumerable<FlashcardResponse?>> GetAllFlashCardByDeck(int deckId);
    Task<FlashcardResponse?> CreateFlashcard(FlashcardRequest flashcardRequest);
}