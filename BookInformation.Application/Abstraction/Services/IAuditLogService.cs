using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Services;

public interface IAuditLogService
{
    Task<PagedResult<AuditLogDto>> GetAsync(AuditLogQueryDto dto, CancellationToken cancellationToken);
    Task AddAsync(AuditLog log, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<AuditLog> logs, CancellationToken cancellationToken);
}