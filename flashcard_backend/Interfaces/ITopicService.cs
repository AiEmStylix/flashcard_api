using flashcard_backend.DTOs;

namespace flashcard_backend.Interfaces;

public interface ITopicService
{
    Task<IEnumerable<TopicResponseDto>> GetAllAsync();
    Task<TopicResponseDto> CreateAsync(TopicRequestDto requestDto);
    Task<TopicResponseDto> GetByIdAsync(int vocabularyId);
    Task<TopicResponseDto> UpdateAsync(int topicId, TopicRequestDto updateDto);
    Task<bool> DeleteAsync(int topicId);
}