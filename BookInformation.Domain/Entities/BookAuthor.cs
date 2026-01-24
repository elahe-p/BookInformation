namespace BookInformation.Domain.Entities;

public class BookAuthor
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid BookId { get; private set; }
    public Guid AuthorId { get; private set; }

    private BookAuthor() { }
    public BookAuthor(Guid bookId, Guid authorId)
    {
        BookId = bookId;
        AuthorId = authorId;
    }
}