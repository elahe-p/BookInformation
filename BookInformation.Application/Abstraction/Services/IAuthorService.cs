using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Services;

public interface IAuthorService
{
    Task<Guid> CreateAsync(AuthorDto dto, CancellationToken cancellationToken);
    Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    Task<Author> GetByIdAsync(Guid id);
    Task<List<Author>> GetAllAsync(CancellationToken cancellationToken);
    Task ValidateAuthors(List<Guid> authorIds, CancellationToken cancellationToken);
}
