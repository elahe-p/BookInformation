using BookInformation.UI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookInformation.UI.Pages.AuditLogs;

public class IndexModel : PageModel
{
    private readonly HttpClient _client;

    public IndexModel(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("BookApi");
    }

    public string? BookId { get; set; }

    public PagedResult<AuditLog> Logs { get; set; } = new PagedResult<AuditLog>(new List<AuditLog>(), 0, 1, 10);

    public async Task OnGetAsync(string bookId)
    {

        var url = $"http://localhost:5112/api/auditlogs?EntityName=Book&EntityId={bookId}&Descending=true&Page=1&PageSize=10";


        Logs = await _client.GetFromJsonAsync<PagedResult<AuditLog>>(url)
               ?? new PagedResult<AuditLog>(new List<AuditLog>(), 0, 1, 10);

    }
}

