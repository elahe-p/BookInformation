using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Services;

public interface IAuditLogService
{
    AuditLog PropertyChanged(
       string entityName,
       Guid entityId,
       string property,
       string? oldValue,
       string? newValue);

    AuditLog Created(string entityName, Guid entityId);
    Task<PagedResult<AuditLogDto>> GetAsync(AuditLogQueryDto dto);
    Task AddAsync(AuditLog log, CancellationToken cancellationToken);
    Task AddRangeAsync(List<AuditLog> logs, CancellationToken cancellationToken);
}
