using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Domain.Entities;
using BookInformation.Infrastructure.Database;

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

    public async Task AddRangeAsync(List<AuditLog> logs, CancellationToken cancellationToken)
    {
        await _context.AuditLogs.AddRangeAsync(logs, cancellationToken);
    }

    public IQueryable<AuditLog> Query()
    {
        return _context.AuditLogs.AsQueryable();
    }
}
