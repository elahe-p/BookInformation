using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Repositories;

public interface IAuthorRepository
{
    Task AddAsync(Author author, CancellationToken cancellationToken);
    Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    Task<Author> GetByIdAsync(Guid id);
    Task<List<Author>> GetAllAsync(CancellationToken cancellationToken);
    Task<int> CheckAuthorsCountByIds(List<Guid> authorIds, CancellationToken cancellationToken);

}
