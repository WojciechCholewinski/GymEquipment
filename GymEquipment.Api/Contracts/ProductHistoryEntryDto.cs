using GymEquipment.Domain.History;

namespace GymEquipment.Api.Contracts
{
    public record ProductHistoryEntryDto(
     Guid Id,
     Guid ProductId,
     ProductChangeType ChangeType,
     DateTime ChangedAtUtc,
     string? ChangedBy,
     string? ModifiedValue);
}
