using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Repositories;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLog log, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<AuditLog> logs, CancellationToken cancellationToken);
    Task<PagedResult<AuditLog>> GetAsync(AuditLogQueryDto dto, CancellationToken cancellationToken);
}
