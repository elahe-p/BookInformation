using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Services;

public interface IBookService
{
    Task<Guid> CreateAsync(BookDto dto, CancellationToken cancellationToken);
    Task<BookInfoDto?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken);
    Task UpdateAsync(Guid bookId, BookDto dto, CancellationToken cancellationToken);
}
