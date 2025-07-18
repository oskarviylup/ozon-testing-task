using project.Application.Abstractions.Repositories;
using project.Application.Contracts;
using project.Application.Models;

namespace Presentation.Kafka.Consumer;

public class ReportRequestHandler(IItemViewsRepository itemViewsRepository,
        IItemPaymentsRepository itemPaymentsRepository, IReportRequestRepository reportRequestRepository)
    : IReportRequestHandler
{
    public Task GenerateReport()
    {
        throw new NotImplementedException();
    }

    public async Task HandleReportRequestAsync(KafkaReportRequest request)
    {
        var itemViewsAmount = await itemViewsRepository.GetItemViewsByItemIdInSomePeriod(
            request.RequestParameters.ItemId,
            request.RequestParameters.ConversionCheckPeriodStart,
            request.RequestParameters.ConversionCheckPeriodEnd);

        var itemPaymentsAmount = await itemPaymentsRepository.GetItemPaymentsByItemIdInSomePeriod(
            request.RequestParameters.ItemId,
            request.RequestParameters.ConversionCheckPeriodStart,
            request.RequestParameters.ConversionCheckPeriodEnd);

        var conversion = itemViewsAmount == 0 ? 0 : (decimal)itemPaymentsAmount / itemViewsAmount;

        await reportRequestRepository.AddReportInfo(request.Id, conversion, itemPaymentsAmount);
    }
}