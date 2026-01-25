namespace BookInformation.Domain.Primitives;

public sealed record PropertyChange(
    string Property,
    string? OldValue,
    string? NewValue);
