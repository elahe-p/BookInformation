using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookInformation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuthorCreateDto dto, CancellationToken cancellationToken = default)
    {
        var authorId = await _authorService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = authorId }
        );
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        var book = await _authorService.GetAllAsync(cancellationToken);
        return Ok(book);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await _authorService.GetByIdAsync(id);
        return Ok(author);
    }
}
