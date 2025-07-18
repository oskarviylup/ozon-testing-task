using Microsoft.EntityFrameworkCore;
using project.Application;
using project.Application.Abstractions;
using project.Application.Abstractions.Repositories;

namespace project.Infrastructure.Persistence.Repositories;

public class ItemViewsRepository(ApplicationContext db) : IItemViewsRepository
{
    public async Task<int> GetItemViewsByItemIdInSomePeriod(Guid itemId, DateOnly periodStart, DateOnly periodEnd)
    {
        var startDate = DateTime.SpecifyKind(periodStart.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        var endDate = DateTime.SpecifyKind(periodEnd.ToDateTime(TimeOnly.MaxValue), DateTimeKind.Utc);

        return await db.ItemViews
            .Where(v => v.ItemId == itemId &&
                        v.ViewedAt >= startDate &&
                        v.ViewedAt <= endDate)
            .CountAsync();
    }
}