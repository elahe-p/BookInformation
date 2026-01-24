using BookInformation.Domain.Enums;

namespace BookInformation.Application.DTOs;

public record AuditLogQueryDto(
    string EntityType,
    Guid EntityId,
    AuditActionEnum? Action,
    string? PropertyName,
    int Page = 1,
    int PageSize = 20,
    bool Descending = true
);
