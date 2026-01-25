using System.ComponentModel.DataAnnotations;
using BookInformation.Application.Abstraction;
using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IApplicationDbContext _dbContext;


    public AuthorService(IAuthorRepository authorRepository, IApplicationDbContext dbContext)
    {
        _authorRepository = authorRepository;
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateAsync(AuthorDto dto, CancellationToken cancellationToken)
    {
        var author = new Author(dto.FirstName, dto.LastName);

        await _authorRepository.AddAsync(author, cancellationToken);
     
        await _dbContext.SaveChangesAsync(cancellationToken);

        return author.Id;
    }

    public async Task<List<Author>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAllAsync(cancellationToken);

    }

    public async Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetByIdsAsync(ids, cancellationToken);
    }

    public async Task<Author> GetByIdAsync(Guid id)
    {
        return await _authorRepository.GetByIdAsync(id);

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
