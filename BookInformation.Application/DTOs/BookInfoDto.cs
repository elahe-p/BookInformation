namespace BookInformation.Application.DTOs;

public record BookInfoDto(
    Guid Id,
    string Title,
    string Description,
    DateOnly PublishDate,
    IEnumerable<Guid> AuthorIds,
    string AuthorNames
);
