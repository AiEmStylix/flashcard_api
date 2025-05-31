using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;

namespace flashcard_backend.Services;

public class VocabularyService : IVocabularyService
{
    private IVocabularyRepository _vocabularyRepository;

    public VocabularyService(IVocabularyRepository vocabularyRepository)
    {
        _vocabularyRepository = vocabularyRepository;
    }

    public async Task<IEnumerable<Vocabulary>> GetAllAsync()
    {
        var vocabulary = await _vocabularyRepository.GetAllAsync();
        if (vocabulary is null)
        {
            return null;
        }
        return vocabulary;
    }

    public async Task<VocabularyDto> CreateAsync(VocabularyCreateDto createDto)
    {
        if (string.IsNullOrWhiteSpace(createDto.Word))
        {
            return null;
        }
        
        var vocabularyEntity = new Vocabulary
        {
            Word = createDto.Word,
            Definition = createDto.Definition,
            TopicId = createDto.TopicId, // You can change this to use a dynamic topic if needed
        };
        
        var addedVocabulary = await _vocabularyRepository.AddAsync(vocabularyEntity);
        
        var vocabularyDto = new VocabularyDto
        {
            Word = addedVocabulary.Word,
            Definition = addedVocabulary.Definition,
            TopicId = addedVocabulary.TopicId,
            CreatedAt = addedVocabulary.CreatedAt,
        };

        return vocabularyDto;
    }

    public Task<VocabularyDto> GetByIdAsync(int vocabularyId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VocabularyDto>> GetByTopicAsync(int topicId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VocabularyDto>> GetByDifficultyAsync(int difficulty)
    {
        throw new NotImplementedException();
    }

    public Task<VocabularyDto> UpdateAsync(int vocabularyId, VocabularyCreateDto updateDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int vocabularyId)
    {
        throw new NotImplementedException();
    }
}