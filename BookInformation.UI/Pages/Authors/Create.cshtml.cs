using BookInformation.UI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CreateAuthorModel : PageModel
{
    private readonly HttpClient _client;

    public CreateAuthorModel(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("BookApi");
    }

    [BindProperty]
    public CreateAuthorDto Author { get; set; } = new("", "");

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _client.PostAsJsonAsync("/api/authors", Author);

        return RedirectToPage("/Books/Index");
    }
}
