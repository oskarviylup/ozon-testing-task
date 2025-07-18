using Moq;
using Presentation.Kafka.Producer;
using project.Application;
using project.Application.Abstractions;
using project.Application.Abstractions.Repositories;
using project.Application.Models.DbModels;
using project.Application.Services;
using Xunit;

namespace project.Tests.Services;

public class UserServiceTests
{
    [Fact]
    public async Task ChargeOffRequestFeeFromUserBalance_Should_Decrease_Amount()
    {
        var userId = Guid.NewGuid();
        var balance = new Balance { Id = Guid.NewGuid(), UserId = userId, Amount = 5 };

        var repoMock = new Mock<IBalanceRepository>();
        repoMock.Setup(r => r.GetUserBalanceById(userId)).ReturnsAsync(balance.Amount);
        
        var kafkaMock = new Mock<IReportRequestKafkaProducer>();
        var reportRepoMock = new Mock<IReportRequestRepository>();
        var userRepoMock = new Mock<IUserRepository>();
        var userService = new UserService(
            userRepoMock.Object,
            kafkaMock.Object,
            reportRepoMock.Object,
            repoMock.Object);
        
        await userService.ChargeOffRequestFeeFromUserBalance(userId);
        
        Assert.Equal(4, balance.Amount);
    }

    [Fact]
    public async Task ChargeOffRequestFeeFromUserBalance_Should_Throw_If_User_Not_Found()
    {
        var userId = Guid.NewGuid();

        var repoMock = new Mock<IBalanceRepository>();
        repoMock.Setup(r => r.GetUserBalanceById(userId)).ReturnsAsync(null);

        var kafkaMock = new Mock<IReportRequestKafkaProducer>();
        var reportRepoMock = new Mock<IReportRequestRepository>();
        var userRepoMock = new Mock<IUserRepository>();
        var userService = new UserService(
            userRepoMock.Object,
            kafkaMock.Object,
            reportRepoMock.Object,
            repoMock.Object);
        
        await Assert.ThrowsAsync<Exception>(() =>
            userService.ChargeOffRequestFeeFromUserBalance(userId));
    }

    [Fact]
    public async Task ChargeOffRequestFeeFromUserBalance_Should_Throw_If_Balance_Is_Zero()
    {
        var userId = Guid.NewGuid();
        var balance = new Balance { Id = Guid.NewGuid(), UserId = userId, Amount = 0 };

        var repoMock = new Mock<IBalanceRepository>();
        repoMock.Setup(r => r.GetUserBalanceById(userId)).ReturnsAsync(balance.Amount);

        var kafkaMock = new Mock<IReportRequestKafkaProducer>();
        var reportRepoMock = new Mock<IReportRequestRepository>();
        var userRepoMock = new Mock<IUserRepository>();
        var userService = new UserService(
            userRepoMock.Object,
            kafkaMock.Object,
            reportRepoMock.Object,
            repoMock.Object);
        
        await Assert.ThrowsAsync<Exception>(() =>
            userService.ChargeOffRequestFeeFromUserBalance(userId));
    }
}