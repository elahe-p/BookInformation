namespace BookInformation.Application.DTOs;

public record BookDto(
    string Title,
    string Description,
    DateOnly PublishDate,
    List<Guid> AuthorIds
);