namespace BookInformation.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateOnly PublishDate { get; private set; }
    public ICollection<BookAuthor> Authors { get; set; } = new List<BookAuthor>();

    private Book() { }

    public Book(string title, string description, DateOnly publishDate)
    {
        Title = title;
        Description = description;
        PublishDate = publishDate;
    }

    public void Update(string title, string description, DateOnly publishDate)
    {
        Title = title;
        Description = description;
        PublishDate = publishDate;
    }

}