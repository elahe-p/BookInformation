namespace BookInformation.Domain.Entities;

public class BookAuthor
{
    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public Guid AuthorId { get; private set; }
}