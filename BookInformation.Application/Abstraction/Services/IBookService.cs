using BookInformation.Application.DTOs;

namespace BookInformation.Application.Abstraction.Services;

public interface IBookService
{
    Task<Guid> CreateAsync(BookDto dto, CancellationToken cancellationToken);
    Task<BookInfoDto?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken);
    Task<List<BookInfoDto>> GetAllAsync(CancellationToken cancellationToken);
    Task UpdateAsync(Guid bookId, BookDto dto, CancellationToken cancellationToken);
}
