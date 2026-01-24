using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;
using BookInformation.Domain.Primitives;

namespace BookInformation.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorService _authorService;


    public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork, IAuthorService authorService)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _authorService = authorService;
    }

    public async Task<Guid> CreateAsync(BookDto dto, CancellationToken cancellationToken)
    {
        await _authorService.ValidateAuthors(dto.AuthorIds, cancellationToken);

        var book = new Book(dto.Title, dto.Description, dto.PublishDate);

        //Add Book Author

        await _bookRepository.AddAsync(book, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }

    public async Task<BookInfoDto?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdWithAuthorsAsync(bookId, cancellationToken);

        var bookinfo = new BookInfoDto(book.Id, book.Title, book.Description, book.PublishDate, book.Authors.Select(a => a.AuthorId));

        return bookinfo;
    }

    public async Task UpdateAsync(Guid bookId, BookDto dto, CancellationToken cancellationToken)
    {
        Book? book = await _bookRepository.GetByIdWithAuthorsAsync(bookId, cancellationToken)
            ?? throw new NotFoundException($"Book '{bookId}' not found");

        await _authorService.ValidateAuthors(dto.AuthorIds, cancellationToken);

        book.Update(dto.Title, dto.Description, dto.PublishDate);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
