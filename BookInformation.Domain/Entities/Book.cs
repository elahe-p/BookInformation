namespace BookInformation.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateOnly PublishDate { get; private set; }

    public ICollection<BookAuthor> Authors {get; set;} = new List<BookAuthor>();
}