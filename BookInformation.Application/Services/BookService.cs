using BookInformation.Application.Abstraction;
using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;
using BookInformation.Domain.Enums;
using BookInformation.Domain.Primitives;

namespace BookInformation.Application.Services;

public class BookService : IBookService
{
    private const string EntityName = "Book";
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorService _authorService;
    private readonly IAuditLogService _auditLogService;
    private readonly IApplicationDbContext _dbContext;

    public BookService(IBookRepository bookRepository, IAuthorService authorService, IAuditLogService auditLogService, IApplicationDbContext dbContext)
    {
        _bookRepository = bookRepository;
        _authorService = authorService;
        _auditLogService = auditLogService;
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateAsync(BookDto dto, CancellationToken cancellationToken)
    {
        await _authorService.ValidateAuthors(dto.AuthorIds, cancellationToken);

        var book = new Book(dto.Title, dto.Description, dto.PublishDate);

        book.SetAuthors(dto.AuthorIds);

        await _bookRepository.AddAsync(book, cancellationToken);

        var auditLog = new AuditLog(
                EntityName,
                book.Id,
                AuditActionEnum.Created,
                $"Book '{book.Title}' was created");

        await _auditLogService.AddAsync(auditLog, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return book.Id;
    }

    public async Task UpdateAsync(Guid bookId, BookDto dto, CancellationToken cancellationToken)
    {
        Book? book = await _bookRepository.GetByIdWithAuthorsAsync(bookId, cancellationToken)
            ?? throw new NotFoundException($"Book '{bookId}' not found");

        await _authorService.ValidateAuthors(dto.AuthorIds, cancellationToken);


        var changes = new List<PropertyChange>();
        changes.AddRange(book.UpdateDetails(
            dto.Title,
            dto.Description,
            dto.PublishDate));

        changes.AddRange(book.SetAuthors(dto.AuthorIds));

        if (changes.Any())
        {
            var audits = changes.Select(change => new AuditLog(
               EntityName,
               book.Id,
               AuditActionEnum.Updated,
               change.Property,
               change.OldValue,
               change.NewValue));

            await _auditLogService.AddRangeAsync(audits, cancellationToken);

        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<BookInfoDto?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdWithAuthorsAsync(bookId, cancellationToken) ?? throw new NotFoundException($"Book '{bookId}' not found"); ;

        var bookinfo = new BookInfoDto(book.Id, book.Title, book.Description, book.PublishDate, book.Authors.Select(a => a.AuthorId));

        return bookinfo;
    }
}
