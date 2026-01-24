using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;
using BookInformation.Domain.Enums;

namespace BookInformation.Application.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _repository;

    public AuditLogService(IAuditLogRepository repository)
    {
        _repository = repository;
    }

    public AuditLog PropertyChanged(
       string entityName,
       Guid entityId,
       string property,
       string? oldValue,
       string? newValue)
    {
        return new AuditLog(entityName, entityId, AuditActionEnum.Updated, property, oldValue, newValue);
    }

    public AuditLog Created(string entityName, Guid entityId)
    {
        return new AuditLog(entityName, entityId, AuditActionEnum.Created, "Entity");
    }

    public async Task<PagedResult<AuditLogDto>> GetAsync(AuditLogQueryDto dto)
    {
        IQueryable<AuditLog> query = _repository.Query().Where(i =>
           i.EntityName == dto.EntityType &&
           i.EntityId == dto.EntityId);


        if (dto.Action.HasValue)
            query = query.Where(i => i.Action == dto.Action);

        if (!string.IsNullOrWhiteSpace(dto.PropertyName))
            query = query.Where(i => i.PropertyName == dto.PropertyName);

        var totalCount = query.Count();

        query = dto.Descending
            ? query.OrderByDescending(i => i.ChangedAt)
            : query.OrderBy(i => i.ChangedAt);


        List<AuditLogDto>? items = query
            .Skip((dto.Page - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .Select(i => new AuditLogDto(
                i.Action,
                ChangedAt: i.ChangedAt,
                PropertyName: i.PropertyName,
                OldValue: i.OldValue,
                NewValue: i.NewValue
            )).ToList();

        return new PagedResult<AuditLogDto>(items, totalCount);
    }

    public async Task AddAsync(AuditLog log, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(log, cancellationToken);
    }

    public async Task AddRangeAsync(List<AuditLog> logs, CancellationToken cancellationToken)
    {
        await _repository.AddRangeAsync(logs, cancellationToken);
    }
}
