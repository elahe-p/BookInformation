using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Services;

public interface IAuthorService
{
    Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    Task ValidateAuthors(List<Guid> authorIds, CancellationToken cancellationToken);
    void ApplyAuthorChanges(Book book, IEnumerable<Guid> authorIds, List<AuditLog> audits);
}
