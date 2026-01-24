using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Repositories;

public interface IAuthorRepository
{
    Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    Task<int> CheckAuthorsCountByIds(List<Guid> authorIds, CancellationToken cancellationToken);

}
