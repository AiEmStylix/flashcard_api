using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;

namespace flashcard_backend.Services;

public class TopicService : ITopicService
{
    private readonly ITopicRepository _topicRepository;

    public TopicService(ITopicRepository topicRepository)
    {
        _topicRepository = topicRepository;
    }

    public async Task<IEnumerable<TopicResponseDto>> GetAllAsync()
    {
        var topics = await _topicRepository.GetAllAsync();
        if (topics is null)
        {
            return Enumerable.Empty<TopicResponseDto>();
        }

        return topics.Select(t => new TopicResponseDto
        {
            TopicId = t.TopicId,
            Name = t.Name,
            CreatedAt = t.CreatedAt,
            Description = t.Description,
            IsActive = t.IsActive,
        });
    }

    public async Task<TopicResponseDto> CreateAsync(TopicRequestDto requestDto)
    {
        if (string.IsNullOrWhiteSpace(requestDto.Name) || string.IsNullOrWhiteSpace(requestDto.Description))
        {
            return null;
        }

        var newTopic = new Topic
        {
            /*
             * ID and CreatedAt got handle at the model,
             * So no need to explicit here
             */
            Name = requestDto.Name,
            Description = requestDto.Description,
            IsActive = true,
        };
        var newTopicEntity = await _topicRepository.AddAsync(newTopic);
        return new TopicResponseDto
        {
            Name = newTopicEntity.Name,
            Description = newTopicEntity.Description,
            IsActive = newTopicEntity.IsActive,
            CreatedAt = newTopicEntity.CreatedAt,
            TopicId = newTopicEntity.TopicId,
        };
    }

    public async Task<TopicResponseDto> GetByIdAsync(int topicId)
    {
        var topic = await _topicRepository.GetByIdAsync(topicId);
        if (topic is null)
        {
            return null;
        }

        return new TopicResponseDto
        {
            Name = topic.Name,
            Description = topic.Description,
            IsActive = topic.IsActive,
            CreatedAt = topic.CreatedAt,
            TopicId = topic.TopicId,
        };
    }

    public async Task<TopicResponseDto> UpdateAsync(int topicId, TopicRequestDto updateDto)
    {
        var existTopic = await _topicRepository.GetByIdAsync(topicId);
        if (existTopic is null)
        {
            return null;
        }
        
        if (!string.IsNullOrWhiteSpace(updateDto.Name))
        {
            existTopic.Name = updateDto.Name;
        }

        if (!string.IsNullOrWhiteSpace(updateDto.Description))
        {
            existTopic.Description = updateDto.Description;
        }

        if (updateDto.IsActive.HasValue)
        {
            existTopic.IsActive = updateDto.IsActive.Value;
        }
        
        //Handle update
        var updateTopic = await _topicRepository.UpdateAsync(existTopic);
        return new TopicResponseDto
        {
            Name = updateTopic.Name,
            Description = updateTopic.Description,
            IsActive = updateTopic.IsActive,
            CreatedAt = updateTopic.CreatedAt,
            TopicId = updateTopic.TopicId,
        };
    }

    public async Task<bool> DeleteAsync(int topicId)
    {
        return await _topicRepository.DeleteAsync(topicId);
    }
}