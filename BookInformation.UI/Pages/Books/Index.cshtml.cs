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
        // Books = await _httpClient.GetFromJsonAsync<IEnumerable<Book>>("http://localhost:5112/api/Books") 
        //         ?? Enumerable.Empty<Book>();
        // AuditLogs = await _httpClient.GetFromJsonAsync<IEnumerable<AuditLog>>("http://localhost:5112/api/auditlogs")
        //         ?? Enumerable.Empty<AuditLog>();
    }
}
