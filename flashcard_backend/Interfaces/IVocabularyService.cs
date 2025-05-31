using flashcard_backend.DTOs;
using flashcard_backend.Models;

namespace flashcard_backend.Interfaces;

public interface IVocabularyService
{
    Task<IEnumerable<VocabularyResponseDto>> GetAllAsync();
    Task<VocabularyResponseDto> CreateAsync(VocabularyRequestDto requestDto);
    Task<VocabularyResponseDto> GetByIdAsync(int vocabularyId);
    Task<IEnumerable<VocabularyResponseDto>> GetByTopicAsync(int topicId);
    Task<IEnumerable<VocabularyResponseDto>> GetByDifficultyAsync(int difficulty);
    Task<VocabularyResponseDto> UpdateAsync(int vocabularyId, VocabularyRequestDto updateDto);
    Task<bool> DeleteAsync(int vocabularyId);
}