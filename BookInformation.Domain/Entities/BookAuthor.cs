namespace BookInformation.Domain.Entities;

public class BookAuthor
{
    public Guid BookId { get; private set; }
    public Guid AuthorId { get; private set; }
    public Author Author { get; set; } 

    private BookAuthor() { }
    public BookAuthor(Guid bookId, Guid authorId)
    {
        BookId = bookId;
        AuthorId = authorId;
    }
}