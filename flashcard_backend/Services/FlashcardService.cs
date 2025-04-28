using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;

namespace flashcard_backend.Services;

public class FlashcardService : IFlashcardService
{
    private readonly IFlashcardRepository _flashcardRepository;

    public FlashcardService(IFlashcardRepository flashcardRepository)
    {
        _flashcardRepository = flashcardRepository;
    }

    public async Task<IEnumerable<FlashcardResponse?>> GetAllFlashcard()
    {
        var flashcards = await _flashcardRepository.GetAllFlashcardAsync();

        return flashcards.Select(flashcard => new FlashcardResponse
        {
            Id = flashcard.Id,
            Question = flashcard.Question,
            Answer = flashcard.Answer,
            CreatedAt = flashcard.CreatedAt,
            DeckId = flashcard.DeckId
        }).ToList();
    }

    public async Task<FlashcardResponse?> GetFlashcardById(int id)
    {
        var flashcard = await _flashcardRepository.GetFlashcardByIdAsync(id);
        if (flashcard is null)
        {
            return null;
        }

        return new FlashcardResponse
        {
            Id = flashcard.Id,
            Question = flashcard.Question,
            Answer = flashcard.Answer,
            CreatedAt = flashcard.CreatedAt,
            DeckId = flashcard.DeckId,
        };
    }

    public async Task<IEnumerable<FlashcardResponse?>> GetAllFlashCardByDeck(int deckId)
    {
        var flashcards = await _flashcardRepository.GetFlashcardsByDeckId(deckId);
        if (flashcards is null)
        {
            return null;
        }

        return flashcards.Select(flashcard => new FlashcardResponse
        {
            Id = flashcard.Id,
            Question = flashcard.Question,
            Answer = flashcard.Answer,
            CreatedAt = flashcard.CreatedAt,
            DeckId = flashcard.DeckId,
        }).ToList();
    }
}