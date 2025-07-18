using Moq;
using Presentation.Kafka.Producer;
using project.Application.Abstractions;
using project.Application.Abstractions.Repositories;
using project.Application.Contracts;
using project.Application.Models;
using project.Application.Models.DbModels;
using project.Application.Services;
using Xunit;

namespace project.Tests.Kafka;

public class KafkaProducerTests
{
    [Fact]
    public async Task RequestReport_Should_Call_KafkaProducer_With_ValidRequest()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var reportRequest = new KafkaReportRequest(new ReportRequestParameters
        {
            ConversionCheckPeriodStart = DateOnly.FromDateTime(DateTime.UtcNow),
            ConversionCheckPeriodEnd = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            ItemId = Guid.NewGuid(),
            OrderId = Guid.NewGuid()
        }) { UserId = userId };

        var balance = new Balance { UserId = userId, Amount = 5 };

        var repoMock = new Mock<IBalanceRepository>();
        repoMock.Setup(r => r.GetUserBalanceById(userId)).ReturnsAsync(balance.Amount);

        var kafkaMock = new Mock<IReportRequestKafkaProducer>();
        var reportRepoMock = new Mock<IReportRequestRepository>();
        var userRepoMock = new Mock<IUserRepository>();
        var service = new UserService(
            userRepoMock.Object,
            kafkaMock.Object,
            reportRepoMock.Object,
            repoMock.Object);

        // Act
        await service.RequestReport(userId, reportRequest);

        // Assert
        kafkaMock.Verify(k => k.ProduceAsync(
            It.Is<KafkaReportRequest>(r =>
                r.UserId == userId &&
                r.RequestParameters.ItemId == reportRequest.RequestParameters.ItemId
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}