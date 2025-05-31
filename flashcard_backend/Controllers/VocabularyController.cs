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
        var vocabulary = _vocabularyService.GetAllAsync();
        return Ok(vocabulary);
    }
    
    [HttpPost]
    public async Task<ActionResult<VocabularyDto>> Create([FromBody] VocabularyCreateDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _vocabularyService.CreateAsync(createDto);

        if (result == null)
        {
            return BadRequest("Invalid vocabulary data.");
        }

        // Return 201 Created with the location of the new resource
        return Ok(result);
    }
    
}