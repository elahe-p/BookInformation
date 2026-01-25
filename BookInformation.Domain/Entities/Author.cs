namespace BookInformation.Domain.Entities;

public class Author
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;

    private Author() { }
    public Author(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
