using Microsoft.EntityFrameworkCore;
using project.Application;
using project.Application.Abstractions.Repositories;
using project.Application.Models.DbModels;

namespace project.Infrastructure.Persistence.Repositories;

public class ReportRequestRepository(ApplicationContext db) : IReportRequestRepository
{
    public async Task CreateReportRequest(ReportRequest reportRequest)
    {
        await db.ReportsRequests.AddAsync(reportRequest);
        await db.SaveChangesAsync();
    }

    public async Task<ReportRequest> ShowReportRequest(Guid reportId) => await db.ReportsRequests.FindAsync(reportId) ?? throw new InvalidOperationException();

    public async Task<bool> FindAnyTodayReportRequestByUserId(Guid userId, DateOnly? date = null)
    {
        var targetDate = date ?? DateOnly.FromDateTime(DateTime.UtcNow);
        
        return await db.ReportsRequests.AnyAsync(r =>
            r.UserId == userId && DateOnly.FromDateTime(r.DateTimeCreated.Date) == targetDate );
    }

    public async Task AddReportInfo(Guid requestId, decimal conversion, int itemPaymentsAmount)
    {
        var request = await db.ReportsRequests.FindAsync(requestId) ?? throw new Exception("Request now found");

        request.Conversion = conversion;
        request.ItemPaymentsAmount = itemPaymentsAmount;
        request.Status = "Completed";

        await db.SaveChangesAsync();
    }
}