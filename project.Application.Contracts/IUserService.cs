using project.Application.Models;
using project.Application.Models.DbModels;

namespace project.Application.Contracts;

public interface IUserService
{
    public Task CreateUser(User user);
    public Task ChargeUserBalance(Guid userId, int value);
    public Task RequestReport(Guid userId, KafkaReportRequest reportRequest);
    public Task ChargeOffRequestFeeFromUserBalance(Guid userId);
}