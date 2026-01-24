using BookInformation.Domain.Enums;

namespace BookInformation.Application.DTOs;

public record AuditLogQueryDto(
    string EntityType,
    Guid EntityId,
    AuditActionEnum? Action,
    string? PropertyName,
    int Page,
    int PageSize,
    bool Descending
);
