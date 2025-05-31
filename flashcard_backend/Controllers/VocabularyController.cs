using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace flashcard_backend.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class VocabularyController : ControllerBase
{
    private readonly IVocabularyService _vocabularyService;


    public VocabularyController(IVocabularyService vocabularyService)
    {
        _vocabularyService = vocabularyService;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllVocabularies()
    {
        var vocabulary = await _vocabularyService.GetAllAsync();
        return Ok(vocabulary);
    }
    
    [HttpPost]
    public async Task<ActionResult<VocabularyRequestDto>> Create([FromBody] VocabularyRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _vocabularyService.CreateAsync(requestDto);

        if (result == null)
        {
            return BadRequest("Invalid vocabulary data.");
        }

        // Return 201 Created with the location of the new resource
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VocabularyRequestDto>> GetVocabulary(int id)
    {
        var vocabulary = await _vocabularyService.GetByIdAsync(id);
    
        if (vocabulary == null)
        {
            return NotFound($"Vocabulary with ID {id} not found");
        }
    
        return Ok(vocabulary);
    }

    [HttpGet("topic/{topicId}")]
    public async Task<ActionResult<VocabularyRequestDto>> GetVocabularyByTopicId(int topicId)
    {
        var vocabulary = await _vocabularyService.GetByTopicAsync(topicId);
        if (vocabulary == null)
        {
            return NotFound($"Vocabulary with topic ID {topicId} not found");
        }
        return Ok(vocabulary);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] VocabularyRequestDto requestDto)
    {
        var updatedVocabulary = await _vocabularyService.UpdateAsync(id, requestDto);
        if (updatedVocabulary == null)
        {
            return NotFound($"Vocabulary with ID {id} not found");
        }
        return Ok(updatedVocabulary);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var status = await _vocabularyService.DeleteAsync(id);
        if (!status)
        {
            return BadRequest($"Vocabulary with ID {id} not found");
        }
        return Ok($"Vocabulary with ID {id} deleted");
    }
}