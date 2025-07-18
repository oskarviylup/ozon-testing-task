using Microsoft.EntityFrameworkCore;
using project.Application;
using project.Application.Abstractions;

namespace project.Infrastructure.Persistence.Repositories;

public class BalanceRepository(ApplicationContext db) : IBalanceRepository
{
    public async Task<int> GetUserBalanceById(Guid userId)
    {
        var userBalance = await db.Balances.FirstAsync(b => b.UserId == userId) ?? throw new Exception("User not found");
        return userBalance.Amount;
    }
    
    public async Task ChargeUserBalanceById(Guid id, int amount)
    {
        var userBalance = await db.Balances.FirstAsync(b => b.UserId == id) ?? throw new Exception("User not found");

        userBalance.Amount += amount;
        await db.SaveChangesAsync();
    }

    public async Task ChargeOffRequestFeeFromUserBalance(Guid userId)
    {
        var userBalance = await db.Balances.FirstAsync(b => b.UserId == userId) ?? throw new Exception("User not found");

        userBalance.Amount = userBalance.Amount > 0 ? userBalance.Amount-- : throw new Exception("Not enough credits to request report");
        await db.SaveChangesAsync();
    }
}