using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Services;

public interface IAuthorService
{
    Task<Guid> CreateAsync(AuthorCreateDto dto, CancellationToken cancellationToken);
    Task<List<AuthorDto>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    Task<AuthorDto> GetByIdAsync(Guid id);
    Task<List<AuthorDto>> GetAllAsync(CancellationToken cancellationToken);
    Task ValidateAuthors(List<Guid> authorIds, CancellationToken cancellationToken);
}
