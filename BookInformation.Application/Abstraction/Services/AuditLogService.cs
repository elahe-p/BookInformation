using BookInformation.Domain.Entities;
using BookInformation.Domain.Enums;

namespace BookInformation.Application.Abstraction.Services;

public class AuditLogService
{
    public static AuditLog PropertyChanged(
       string entityName,
       Guid entityId,
       string property,
       string? oldValue,
       string? newValue)
    {
        return new AuditLog(entityName, entityId, AuditActionEnum.Updated, property, oldValue, newValue);
    }

    public static AuditLog Created(string entityName, Guid entityId)
    {
        return new AuditLog(entityName, entityId, AuditActionEnum.Created, "Entity");
    }
}
