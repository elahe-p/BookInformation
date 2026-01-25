using BookInformation.UI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookInformation.UI.Pages.Books;

public class IndexModel : PageModel
{
    private readonly HttpClient _client;

    public IndexModel(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("BookApi");;
    }

    public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
    public IEnumerable<AuditLog> AuditLogs { get; set; } = Enumerable.Empty<AuditLog>();

    public async Task OnGetAsync()
    {
        Books = await _client.GetFromJsonAsync<List<Book>>("/api/books")
                ?? new List<Book>();
    }
}
