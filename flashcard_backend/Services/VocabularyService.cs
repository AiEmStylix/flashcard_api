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

    public async Task<IEnumerable<VocabularyResponseDto>> GetAllAsync()
    {
        var vocabulary = await _vocabularyRepository.GetAllAsync();
        if (vocabulary is null)
        {
            return Enumerable.Empty<VocabularyResponseDto>();
        }

        return vocabulary.Select(v => new VocabularyResponseDto
        {
            VocabularyId = v.VocabularyId,
            Word = v.Word,
            Definition = v.Definition,
            TopicId = v.TopicId,
            CreatedAt = v.CreatedAt,
        });
    }

    public async Task<VocabularyResponseDto> CreateAsync(VocabularyRequestDto requestDto)
    {
        if (string.IsNullOrWhiteSpace(requestDto.Word))
        {
            return null;
        }
        
        var vocabularyEntity = new Vocabulary
        {
            Word = requestDto.Word,
            Definition = requestDto.Definition,
            TopicId = requestDto.TopicId, // You can change this to use a dynamic topic if needed
        };
        
        var addedVocabulary = await _vocabularyRepository.AddAsync(vocabularyEntity);

        return new VocabularyResponseDto
        {
            VocabularyId = addedVocabulary.VocabularyId,
            Word = addedVocabulary.Word,
            Definition = addedVocabulary.Definition,
        };
    }

    public async Task<VocabularyResponseDto> GetByIdAsync(int vocabularyId)
    {
        var vocabulary = await _vocabularyRepository.GetByIdAsync(vocabularyId);
        if (vocabulary is null)
        {
            return null;
        }

        return new VocabularyResponseDto
        {
            VocabularyId = vocabulary.VocabularyId,
            Word = vocabulary.Word,
            Definition = vocabulary.Definition,
            CreatedAt = vocabulary.CreatedAt,
            TopicId = vocabulary.TopicId,
        };
    }

    public async Task<IEnumerable<VocabularyResponseDto>> GetByTopicAsync(int topicId)
    {
        var vocabulary = await _vocabularyRepository.GetByTopicIdAsync(topicId);
        if (vocabulary is null)
        {
            return Enumerable.Empty<VocabularyResponseDto>();
        }

        return vocabulary.Select(v => new VocabularyResponseDto
        {
            VocabularyId = v.VocabularyId,
            Word = v.Word,
            Definition = v.Definition,
            TopicId = v.TopicId,
            CreatedAt = v.CreatedAt,
        });
    }

    public async Task<IEnumerable<VocabularyResponseDto>> GetByDifficultyAsync(int difficulty)
    {
        throw new NotImplementedException();
    }

    public async Task<VocabularyResponseDto> UpdateAsync(int vocabularyId, VocabularyRequestDto updateDto)
    {
        var vocabulary = await _vocabularyRepository.GetByIdAsync(vocabularyId);
        if (vocabulary is null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(updateDto.Word) || string.IsNullOrWhiteSpace(updateDto.Definition))
        {
            return null;
        }

        vocabulary.Word = updateDto.Word;
        vocabulary.Definition = updateDto.Definition;
        vocabulary.TopicId = updateDto.TopicId;
        var vocabularyEntity = await _vocabularyRepository.UpdateAsync(vocabulary);
        return new VocabularyResponseDto
        {
            VocabularyId = vocabularyEntity.VocabularyId,
            Word = vocabularyEntity.Word,
            Definition = vocabularyEntity.Definition,
            TopicId = vocabularyEntity.TopicId,
            CreatedAt = vocabularyEntity.CreatedAt,
        };
    }

    public async Task<bool> DeleteAsync(int vocabularyId)
    {
        return await _vocabularyRepository.DeleteAsync(vocabularyId);
    }
}
