using System.Transactions;
using Presentation.Kafka.Producer;
using project.Application.Abstractions;
using project.Application.Abstractions.Repositories;
using project.Application.Contracts;
using project.Application.Models;
using project.Application.Models.DbModels;

namespace project.Application.Services;

public class UserService(IUserRepository userRepository, IReportRequestKafkaProducer producer,
        IReportRequestRepository reportRequestRepository, IBalanceRepository balanceRepository)
    : IUserService
{
    public async Task CreateUser(User user) => await userRepository.CreateUser(user);

    public async Task ChargeUserBalance(Guid userId, int value) 
        => await balanceRepository.ChargeUserBalanceById(userId, value);

    public async Task RequestReport(Guid userId, KafkaReportRequest reportRequest)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
        
        var currentTransaction = Transaction.Current;
        
        if (await EnsureUserCanRequestReport(userId))
        {
            var isAnyReportByUserId = await reportRequestRepository.FindAnyTodayReportRequestByUserId(userId);
            if (!isAnyReportByUserId) await balanceRepository.ChargeOffRequestFeeFromUserBalance(userId);
            var dbReportRequest = new ReportRequest
            {
                Id = reportRequest.Id,
                Conversion = null,
                ConversionCheckPeriodStart = reportRequest.RequestParameters.ConversionCheckPeriodStart,
                ConversionCheckPeriodEnd = reportRequest.RequestParameters.ConversionCheckPeriodEnd,
                UserId = reportRequest.UserId,
                Status = "Pending",
                ItemId = reportRequest.RequestParameters.ItemId,
                OrderId = reportRequest.RequestParameters.OrderId,
                ItemPaymentsAmount = null,
                Error = null
            };
            
            Transaction.Current = currentTransaction;
            
            await reportRequestRepository.CreateReportRequest(dbReportRequest);
            await producer.ProduceAsync(reportRequest);
            
            transaction.Complete();
        }
    }

    public async Task ChargeOffRequestFeeFromUserBalance(Guid userId) =>
        await balanceRepository.ChargeOffRequestFeeFromUserBalance(userId);

    private async Task<bool> EnsureUserCanRequestReport(Guid userId)
    {
        var isAnyReportByUserId = await reportRequestRepository.FindAnyTodayReportRequestByUserId(userId);

        return isAnyReportByUserId || await balanceRepository.GetUserBalanceById(userId) > 0;
    }
}