using Grpc.Core;
using project.Application;

namespace Presentation.Grpc;

public class ReportService(ApplicationContext db) : ReportGrpcService.ReportGrpcServiceBase
{
    public override async Task<ReportResponse> GetReport(GetReportRequest request, ServerCallContext context)
    {
        var requestId = Guid.Parse(request.RequestId);
        
        var reportRequest = await db.ReportsRequests.FindAsync(requestId) 
                            ?? throw new RpcException(new Status (StatusCode.NotFound,
            "Report not found"));

        return new ReportResponse
        {
            Conversion = (double)(reportRequest.Conversion ?? 0),
            ItemPaymentsAmount = reportRequest.ItemPaymentsAmount ?? 0,
            Status = reportRequest.Status
        };


    }
}