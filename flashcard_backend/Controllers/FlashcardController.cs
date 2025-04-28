using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace flashcard_backend.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class FlashcardController : ControllerBase
{
    private readonly IFlashcardService _flashcardService;

    public FlashcardController(IFlashcardService flashcardService)
    {
        _flashcardService = flashcardService;
    }

    [HttpGet()]
    public async Task<ActionResult<FlashcardResponse>> GetAllFlashcard()
    {
        var flashcards = await _flashcardService.GetAllFlashcard();
        if (flashcards is null)
        {
            return NotFound(new { message = "No Flashcard found in the database" });
        }

        return Ok(flashcards);
    }

    [HttpGet("card/{id}")]
    public async Task<ActionResult<FlashcardResponse>> GetFlashcardById(int id)
    {
        var flashcard = await _flashcardService.GetFlashcardById(id);
        if (flashcard is null)
        {
            return NotFound("No flashcard with that id found in the database");
        }

        return Ok(flashcard);
    }

    [HttpGet("deck/{id}")]
    public async Task<ActionResult<FlashcardResponse>> GetFlashCardByDeckId(int id)
    {
        var flashcards = await _flashcardService.GetAllFlashCardByDeck(id);
        if (flashcards is null)
        {
            return NotFound("No flashcard with that deck id found in the database");
        }

        return Ok(flashcards);
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<FlashcardResponse>> CreateFlashcard([FromBody] FlashcardRequest request)
    {
        var response = await _flashcardService.CreateFlashcard(request);
        if (response is null)
        {
            return BadRequest(new {message = "Create new flashcard failed, might be duplicate in database"});
        }

        return Ok(response);
    }
}