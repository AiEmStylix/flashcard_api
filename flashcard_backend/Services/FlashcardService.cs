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

    public async Task<CardModel?> GetFlashcardById(int id)
    {
        return await _flashcardRepository.GetFlashcardByIdAsync(id);
    }
    
    
}