namespace project.Application.Abstractions;

public interface IBalanceRepository
{
    public Task<int> GetUserBalanceById(Guid userId);

    public Task ChargeUserBalanceById(Guid id, int amount);

    public Task ChargeOffRequestFeeFromUserBalance(Guid userId);
}