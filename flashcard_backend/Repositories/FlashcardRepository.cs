using flashcard_backend.DatabaseContext;
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
}