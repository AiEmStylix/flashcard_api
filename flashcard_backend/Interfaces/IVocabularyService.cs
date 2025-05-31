using flashcard_backend.DTOs;
using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IVocabularyService
{
    Task<IEnumerable<Vocabulary>> GetAllAsync();
    Task<VocabularyDto> CreateAsync(VocabularyCreateDto createDto);
    Task<VocabularyDto> GetByIdAsync(int vocabularyId);
    Task<IEnumerable<VocabularyDto>> GetByTopicAsync(int topicId);
    Task<IEnumerable<VocabularyDto>> GetByDifficultyAsync(int difficulty);
    Task<VocabularyDto> UpdateAsync(int vocabularyId, VocabularyCreateDto updateDto);
    Task DeleteAsync(int vocabularyId);
}