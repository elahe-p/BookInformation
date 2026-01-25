using BookInformation.UI.Dtos;
using BookInformation.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
public class EditModel : PageModel
{
    private readonly HttpClient _client;

    public EditModel(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("BookApi");
    }

    [BindProperty]
    public BookDto Book { get; set; } = new("", "", DateOnly.FromDateTime(DateTime.Today), new());

    public List<AuthorDto> AllAuthors { get; set; } = new();

    public List<SelectListItem> AuthorOptions { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Book = await _client.GetFromJsonAsync<BookDto>($"/api/books/{id}")
               ?? throw new Exception("Book not found");

        AllAuthors = await _client.GetFromJsonAsync<List<AuthorDto>>("/api/authors")
                     ?? new List<AuthorDto>();

        AuthorOptions = AllAuthors.Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = $"{a.FirstName} {a.LastName}",
            Selected = Book.AuthorIds.Contains(a.Id)
        }).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        if (!ModelState.IsValid)
            return Page();

        await _client.PutAsJsonAsync($"/api/books/{id}", Book);

        return RedirectToPage("Index");
    }
}
