namespace BookInformation.UI.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public  IEnumerable<Guid> AuthorIds { get; set; } = new List<Guid>();
    public string AuthorNames { get; set; } = string.Empty;
}
