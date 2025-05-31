using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace flashcard_backend.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TopicController : ControllerBase
{
    private readonly ITopicService _topicService;

    public TopicController(ITopicService topicService)
    {
        _topicService = topicService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTopics()
    {
        var topics = await _topicService.GetAllAsync();
        return Ok(topics);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTopic([FromBody] TopicRequestDto topicRequestDto)
    {
        var result = await _topicService.CreateAsync(topicRequestDto);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTopic(int id)
    {
        var topic = await _topicService.GetByIdAsync(id);
        return Ok(topic);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTopic(int id, [FromBody] TopicRequestDto topicRequestDto)
    {
        var result = await _topicService.UpdateAsync(id, topicRequestDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTopic(int id)
    {
        var result = await _topicService.DeleteAsync(id);
        return Ok(result);
    }
}