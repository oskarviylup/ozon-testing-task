using Microsoft.EntityFrameworkCore;
using project.Application;
using project.Application.Abstractions.Repositories;

namespace project.Infrastructure.Persistence.Repositories;

public class ItemPaymentsRepository(ApplicationContext db) : IItemPaymentsRepository
{
    public async Task<int> GetItemPaymentsByItemIdInSomePeriod(Guid itemId, DateOnly periodStart, DateOnly periodEnd)
    {
        var startDate = DateTime.SpecifyKind(periodStart.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        var endDate = DateTime.SpecifyKind(periodEnd.ToDateTime(TimeOnly.MaxValue), DateTimeKind.Utc);

        return await db.ItemPayments
            .Where(p => p.ItemId == itemId &&
                        p.PaidAt >= startDate &&
                        p.PaidAt <= endDate)
            .CountAsync();
    }
}