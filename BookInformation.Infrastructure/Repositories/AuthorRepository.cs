using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Domain.Entities;
using BookInformation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BookInformation.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _context;

    public AuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Author author, CancellationToken cancellationToken)
    {
        await _context.Authors.AddAsync(author, cancellationToken);
    }


    public async Task<int> CheckAuthorsCountByIds(List<Guid> authorIds, CancellationToken cancellationToken)
    {
        return await _context.Authors.CountAsync(a => authorIds.Contains(a.Id));
    }

    public async Task<List<Author>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Authors.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

    }

    public async Task<Author?> GetByIdAsync(Guid id)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Author>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await _context.Authors.Where(a => ids.Contains(a.Id)).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }

}
