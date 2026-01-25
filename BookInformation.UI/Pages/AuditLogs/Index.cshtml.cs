using BookInformation.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace BookInformation.UI.Pages.AuditLogs;

public class IndexModel : PageModel
{
    private readonly HttpClient _client;

    public IndexModel(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("BookApi");
    }

    [FromQuery(Name = "bookId")]
    public string? BookId { get; set; }

    [FromQuery(Name = "page")]
    public int CurrentPage { get; set; } = 1;

    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 10;

    public PagedResult<AuditLog> Logs { get; set; } = new(new List<AuditLog>(), 0, 1, 10);

    public int TotalPages => (int)Math.Ceiling((double)Logs.TotalCount / (PageSize > 0 ? PageSize : 10));
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public async Task OnGetAsync()
    {
        if (CurrentPage < 1) CurrentPage = 1;

        if (string.IsNullOrWhiteSpace(BookId))
        {
            Logs = new PagedResult<AuditLog>(new List<AuditLog>(), 0, CurrentPage, PageSize);
            return;
        }

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["EntityName"] = "Book";
        query["EntityId"] = BookId;
        query["Descending"] = "true";
        query["Page"] = CurrentPage.ToString();
        query["PageSize"] = PageSize.ToString();

        string apiUrl = $"/api/auditlogs?{query}";

        try 
        {
            var result = await _client.GetFromJsonAsync<PagedResult<AuditLog>>(apiUrl);
            if (result != null)
            {
                Logs = result;
            }
        }
        catch
        {
            Logs = new PagedResult<AuditLog>(new List<AuditLog>(), 0, CurrentPage, PageSize);
        }
    }
}