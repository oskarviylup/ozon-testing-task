namespace project.Application.Abstractions.Repositories;

public interface IItemViewsRepository
{
    public Task<int> GetItemViewsByItemIdInSomePeriod(Guid itemId, DateOnly periodStart, DateOnly periodEnd);
}