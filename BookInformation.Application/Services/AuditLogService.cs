using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _repository;

    public AuditLogService(IAuditLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<AuditLogDto>> GetAsync(AuditLogQueryDto dto, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAsync(dto, cancellationToken);

        var items = result.Items.Select(a => new AuditLogDto(
            a.Action,
            a.ChangedAt,
            a.PropertyName,
            a.OldValue,
            a.NewValue)).ToList();

        return new PagedResult<AuditLogDto>(items, result.TotalCount, result.Page, result.PageSize);
    }

    public async Task AddAsync(AuditLog log, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(log, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<AuditLog> logs, CancellationToken cancellationToken)
    {
        await _repository.AddRangeAsync(logs, cancellationToken);
    }

}
