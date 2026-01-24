using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookInformation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookDto dto, CancellationToken cancellationToken)
    {
        var bookId = await _bookService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = bookId },
            null
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] BookDto dto, CancellationToken cancellationToken)
    {

        await _bookService.UpdateAsync(id, dto, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var book = await _bookService.GetByIdAsync(id, cancellationToken);

        if (book == null)
            return NotFound();

        return Ok(book);
    }
}
