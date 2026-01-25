using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BookInformation.Application.Abstraction;
using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;


    public AuthorService(IAuthorRepository authorRepository, IApplicationDbContext dbContext, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(AuthorCreateDto dto, CancellationToken cancellationToken)
    {
        var author = new Author(dto.FirstName, dto.LastName);

        await _authorRepository.AddAsync(author, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return author.Id;
    }

    public async Task<List<AuthorDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<AuthorDto>>(authors);
    }

    public async Task<List<AuthorDto>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetByIdsAsync(ids, cancellationToken);
        return _mapper.Map<List<AuthorDto>>(authors);
    }

    public async Task<AuthorDto> GetByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        return _mapper.Map<AuthorDto>(author);
    }

    /// <summary>
    /// Check all the inserted authorIds exist in the database
    /// </summary>
    public async Task ValidateAuthors(List<Guid> authorIds, CancellationToken cancellationToken)
    {
        var count = await _authorRepository.CheckAuthorsCountByIds(authorIds, cancellationToken);
        if (count != authorIds.Distinct().Count())
        {
            throw new ValidationException("Invalid author Ids");
        }
    }
}
