using BookInformation.Domain.Enums;

namespace BookInformation.Application.DTOs;

public record AuditLogQueryDto(
    string EntityName,
    Guid EntityId,
    AuditActionEnum? Action,
    string? PropertyName,
    DateTimeOffset? From,
    DateTimeOffset? To,
    int Page = 1,
    int PageSize = 20,
    bool Descending = true
);
