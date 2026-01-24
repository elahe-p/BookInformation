using System.ComponentModel.DataAnnotations;
using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Application.Abstraction.Services;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetByIdsAsync(ids, cancellationToken);

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
