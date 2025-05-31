using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IVocabularyRepository: IRepository<Vocabulary>
{
    Task<IEnumerable<Vocabulary>> GetByTopicIdAsync(int topicId);
    Task<Vocabulary> GetByWordAsync(string word, int topicId);
}