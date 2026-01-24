using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Repositories;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLog log, CancellationToken cancellationToken);
    Task AddRangeAsync(List<AuditLog> logs, CancellationToken cancellationToken);
    IQueryable<AuditLog> Query();
}
