using flashcard_backend.DatabaseContext;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;

namespace flashcard_backend.Repositories;

public class TopicRepository : Repository<Topic>, ITopicRepository
{
    private readonly ApplicationDbContext _context;

    public TopicRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}