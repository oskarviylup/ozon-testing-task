namespace project.Application.Abstractions.Repositories;

public interface IItemPaymentsRepository
{
    public Task<int> GetItemPaymentsByItemIdInSomePeriod(Guid itemId, DateOnly periodStart, DateOnly periodEnd);
}