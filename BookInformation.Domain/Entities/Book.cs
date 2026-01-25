using BookInformation.Domain.Primitives;

namespace BookInformation.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateOnly PublishDate { get; private set; }
    public IReadOnlyCollection<BookAuthor> Authors => _authors;
    private readonly List<BookAuthor> _authors = new();

    private Book() { }

    public Book(string title, string description, DateOnly publishDate)
    {
        Title = title;
        Description = description;
        PublishDate = publishDate;
    }

    public IReadOnlyCollection<PropertyChange> UpdateDetails(string title, string description, DateOnly publishDate)
    {
        var changes = new List<PropertyChange>();

        if (Title != title)
            changes.Add(new PropertyChange("Title", Title, title));

        if (Description != description)
            changes.Add(new PropertyChange("Description", Description, description));

        if (PublishDate != publishDate)
            changes.Add(new PropertyChange(
                "PublishDate",
                PublishDate.ToString("yyyy-MM-dd"),
                publishDate.ToString("yyyy-MM-dd")));

        Update(title, description, publishDate);

        return changes;
    }

    public IReadOnlyCollection<PropertyChange> SetAuthors(IEnumerable<Guid> authorIds)
    {
        var changes = new List<PropertyChange>();

        var updated = authorIds.Distinct().ToHashSet();
        var current = _authors.Select(a => a.AuthorId).ToHashSet();

        foreach (var added in updated.Except(current))
        {
            _authors.Add(new BookAuthor(Id, added));
            changes.Add(new PropertyChange("Author", null, added.ToString()));
        }

        foreach (var removed in current.Except(updated))
        {
            var entity = _authors.First(a => a.AuthorId == removed);
            _authors.Remove(entity);
            changes.Add(new PropertyChange("Author", removed.ToString(), null));
        }

        return changes;
    }

    private void Update(string title, string description, DateOnly publishDate)
    {
        Title = title;
        Description = description;
        PublishDate = publishDate;
    }


}
