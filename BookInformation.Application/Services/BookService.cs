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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorService _authorService;
    private readonly IAuditLogService _auditLogService;

    public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork, IAuthorService authorService, IAuditLogService auditLogService)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _authorService = authorService;
        _auditLogService = auditLogService;
    }

    public async Task<Guid> CreateAsync(BookDto dto, CancellationToken cancellationToken)
    {
        await _authorService.ValidateAuthors(dto.AuthorIds, cancellationToken);

        var book = new Book(dto.Title, dto.Description, dto.PublishDate);

        foreach (var authorId in dto.AuthorIds.Distinct())
            book.AddAuthor(authorId);

        await _bookRepository.AddAsync(book, cancellationToken);

        var auditLog = new AuditLog(
                EntityName,
                book.Id,
                AuditActionEnum.Created,
                $"Book '{book.Title}' was created");

        await _auditLogService.AddAsync(auditLog, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }

    public async Task UpdateAsync(Guid bookId, BookDto dto, CancellationToken cancellationToken)
    {
        Book? book = await _bookRepository.GetByIdWithAuthorsAsync(bookId, cancellationToken)
            ?? throw new NotFoundException($"Book '{bookId}' not found");

        await _authorService.ValidateAuthors(dto.AuthorIds, cancellationToken);

        var audits = new List<AuditLog>();
        if (book.Title != dto.Title)
        {
            audits.Add(_auditLogService.PropertyChanged(EntityName, book.Id, "Title", book.Title, dto.Title));
        }

        if (book.Description != dto.Description)
        {
            audits.Add(_auditLogService.PropertyChanged(EntityName, book.Id, "Description", book.Description, dto.Description));
        }

        if (book.PublishDate != dto.PublishDate)
        {
            audits.Add(_auditLogService.PropertyChanged(EntityName, book.Id, "PublishDate", book.PublishDate.ToString("yyyy-MM-dd"), dto.PublishDate.ToString("yyyy-MM-dd")));
        }

        book.Update(dto.Title, dto.Description, dto.PublishDate);

        _authorService.ApplyAuthorChanges(book, dto.AuthorIds, audits);

        if (audits.Count != 0)
        {
            await _auditLogService.AddRangeAsync(audits, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<BookInfoDto?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdWithAuthorsAsync(bookId, cancellationToken);

        var bookinfo = new BookInfoDto(book.Id, book.Title, book.Description, book.PublishDate, book.Authors.Select(a => a.AuthorId));

        return bookinfo;
    }
}
