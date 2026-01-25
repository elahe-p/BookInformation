using BookInformation.Domain.Enums;

namespace BookInformation.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string EntityName { get; private set; } = string.Empty;
    public Guid EntityId { get; private set; }
    public AuditActionEnum Action { get; private set; }
    public string PropertyName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string? OldValue { get; init; }
    public string? NewValue { get; init; }
    public DateTimeOffset ChangedAt { get; private set; } = DateTime.UtcNow;

    private AuditLog() { }

    public AuditLog(string entityName,
        Guid entityId,
        AuditActionEnum action,
        string property,
        string description,
        string? oldValue = null,
        string? newValue = null)
    {
        Id = Guid.NewGuid();
        EntityName = entityName;
        EntityId = entityId;
        Action = action;
        PropertyName = property;
        Description = description;
        OldValue = oldValue;
        NewValue = newValue;
        ChangedAt = DateTimeOffset.UtcNow;
    }
}
