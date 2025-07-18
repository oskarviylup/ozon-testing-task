using project.Application.Models;

namespace project.Application.Contracts;

public interface IReportRequestHandler
{
    public Task HandleReportRequestAsync(KafkaReportRequest request);
    public Task GenerateReport();
}