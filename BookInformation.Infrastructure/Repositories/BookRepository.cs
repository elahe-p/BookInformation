using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Domain.Entities;
using BookInformation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BookInformation.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Book book, CancellationToken cancellationToken)
    {
        await _context.Books.AddAsync(book, cancellationToken);
    }

    public async Task<Book?> GetByIdWithAuthorsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Books
            .Include(b => b.Authors)
            .ThenInclude(ba => ba.Author)
            // .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Book>> GetAllWithAuthorsAsync(CancellationToken cancellationToken)
    {
        return await _context.Books
           .Include(b => b.Authors)
           .ThenInclude(ba => ba.Author)
           .AsNoTracking()
           .ToListAsync(cancellationToken: cancellationToken);
    }
}
