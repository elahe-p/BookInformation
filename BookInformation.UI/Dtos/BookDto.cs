namespace BookInformation.UI.Dtos;

public record BookDto(
    string Title,
    string Description,
    DateOnly PublishDate,
    List<Guid> AuthorIds
);
