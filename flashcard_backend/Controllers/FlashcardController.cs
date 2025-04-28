using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;
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
            return BadRequest(new { message = "No Flashcard found in the database" });
        }

        return Ok(flashcards);
    }
}