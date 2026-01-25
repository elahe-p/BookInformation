using BookInformation.UI.Enums;

namespace BookInformation.UI.Models;

public class AuditLog
{
    public AuditActionEnum Action { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset ChangedAt { get; set; } = DateTime.UtcNow;
    public string PropertyName { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}
