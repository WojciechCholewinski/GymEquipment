namespace GymEquipment.Domain.Common;

public interface IAsyncSpecification<in T>
{
    string ErrorCode { get; }
    string ErrorMessage { get; }

    Task<bool> IsSatisfiedByAsync(T candidate, CancellationToken cancellationToken = default);
}
