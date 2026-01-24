using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Abstraction.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Book?> GetByIdWithAuthorsAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(Book book, CancellationToken cancellationToken);
}