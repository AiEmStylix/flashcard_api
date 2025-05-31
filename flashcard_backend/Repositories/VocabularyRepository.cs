using flashcard_backend.DatabaseContext;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace flashcard_backend.Repositories;

public class VocabularyRepository : Repository<Vocabulary>, IVocabularyRepository
{
    public VocabularyRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public async Task<IEnumerable<Vocabulary>> GetByTopicIdAsync(int topicId)
    {
        return await _dbSet
            .Include(v => v.Topic)
            .Where(v => v.TopicId == topicId)
            .ToListAsync();
    }

    public async Task<Vocabulary> GetByWordAsync(string word, int topicId)
    {
        return await _dbSet
            .Include(v => v.Topic)
            .FirstOrDefaultAsync(v => v.Word == word && v.TopicId == topicId);
    }
}