using BookInformation.UI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

public class CreateModel : PageModel
{
    private readonly HttpClient _client;

    public CreateModel(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("BookApi");
    }

    [BindProperty]
    public BookDto Book { get; set; } = new("", "", DateOnly.FromDateTime(DateTime.Today), new());
    public List<AuthorDto> AllAuthors { get; set; } = new();
    public List<SelectListItem> AuthorOptions { get; set; } = new();

    public async Task OnGetAsync()
    {
        AllAuthors = await _client.GetFromJsonAsync<List<AuthorDto>>("/api/authors")
                     ?? new List<AuthorDto>();

        AuthorOptions = AllAuthors.Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = $"{a.FirstName} {a.LastName}",
        }).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _client.PostAsJsonAsync("/api/books", Book);

        return RedirectToPage("Index");
    }
}
