using BookInformation.Domain.Enums;

namespace BookInformation.Application.DTOs;

public record AuditLogDto(
    AuditActionEnum Action,
    DateTimeOffset ChangedAt,
    string PropertyName,
    string Description,
    string? OldValue,
    string? NewValue
);