using project.Application.Models.DbModels;

namespace project.Application.Abstractions.Repositories;

public interface IReportRequestRepository
{
    public Task CreateReportRequest(ReportRequest reportRequest);

    public Task<ReportRequest> ShowReportRequest(Guid reportId);

    public Task<bool> FindAnyTodayReportRequestByUserId(Guid userId, DateOnly? date = null);

    public Task AddReportInfo(Guid requestId, decimal conversion, int itemPaymentsAmount);
}