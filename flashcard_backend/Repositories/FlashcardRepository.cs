using flashcard_backend.DatabaseContext;
using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace flashcard_backend.Repositories;

public class FlashcardRepository : IFlashcardRepository
{
    private readonly ApplicationDbContext _context;

    public FlashcardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CardModel>> GetAllFlashcardAsync()
    {
        return await _context.FlashCards.ToListAsync();
    }

    public async Task<CardModel> GetFlashcardByIdAsync(int id)
    {
        return await _context.FlashCards.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<CardModel>> GetFlashcardsByDeckId(int deckId)
    {
        return await _context.FlashCards.Where(f => f.DeckId == deckId).ToListAsync();
    }

    public async Task<CardModel> CreateFlashCard(CardModel cardModel)
    {
        bool isExist = await _context.FlashCards.AnyAsync(f => f.Id == cardModel.Id);
        
        //Return if user exist in database
        if (isExist)
        {
            return null;
        }

        _context.FlashCards.Add(cardModel);
        await _context.SaveChangesAsync();
        return cardModel;
    }
}