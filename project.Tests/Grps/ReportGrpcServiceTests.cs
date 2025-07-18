using Moq;
using Presentation.Grpc;
using project.Application;
using project.Application.Abstractions.Repositories;
using project.Application.Models.DbModels;
using Xunit;

namespace project.Tests.Grps;

public class ReportGrpcServiceTests
{
    [Fact]
    public async Task GetReport_Should_Return_ReportResponse_When_Report_Exists()
    {
        var requestId = Guid.NewGuid();
        var report = new ReportRequest
        {
            Id = requestId,
            Conversion = 0.5m,
            ItemPaymentsAmount = 10,
            Status = "Completed"
        };

        var repoMock = new Mock<IReportRequestRepository>();
        repoMock.Setup(r => r.ShowReportRequest(requestId)).ReturnsAsync(report);
        var dbContextMock = new Mock<ApplicationContext>();
        var grpcService = new ReportService(dbContextMock.Object);

        var request = new GetReportRequest { RequestId = requestId.ToString() };

        // Act
        var result = await grpcService.GetReport(request, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0.5, result.Conversion);
        Assert.Equal(10, result.ItemPaymentsAmount);
        Assert.Equal("Completed", result.Status);
    }
}