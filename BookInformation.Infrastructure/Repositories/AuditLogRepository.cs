using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;
using BookInformation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BookInformation.Infrastructure.Repositories;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly ApplicationDbContext _context;

    public AuditLogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AuditLog log, CancellationToken cancellationToken)
    {
        await _context.AuditLogs.AddAsync(log, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<AuditLog> logs, CancellationToken cancellationToken)
    {
        await _context.AuditLogs.AddRangeAsync(logs, cancellationToken);
    }

    public async Task<PagedResult<AuditLog>> GetAsync(AuditLogQueryDto dto, CancellationToken cancellationToken)
    {
        IQueryable<AuditLog> query = _context.AuditLogs
            .Where(a =>
                a.EntityName == dto.EntityName &&
                a.EntityId == dto.EntityId);

        if (dto.Action.HasValue)
            query = query.Where(a => a.Action == dto.Action);

        if (!string.IsNullOrWhiteSpace(dto.PropertyName))
            query = query.Where(a => a.PropertyName == dto.PropertyName);

        var totalCount = await query.CountAsync(cancellationToken);

        query = dto.Descending
            ? query.OrderByDescending(a => a.ChangedAt)
            : query.OrderBy(a => a.ChangedAt);

        var items = await query
            .Skip((dto.Page - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<AuditLog>(
            items,
            totalCount,
            dto.Page,
            dto.PageSize);
    }
}
