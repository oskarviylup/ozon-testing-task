using Microsoft.AspNetCore.Mvc;
using project.Application.Contracts;
using project.Application.Models;

namespace project.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class ReportsController(IUserService userService) : ControllerBase
{
    
    /// <summary>
    /// Requesting new report.
    /// </summary>
    /// <param name="input">Report Request Information</param>
    /// <returns>Requested Report ID</returns>
    [HttpPost]
    public async Task<IActionResult> RequestReport([FromBody] ReportRequestInputDto input)
    {
        var reportRequestParameters = new ReportRequestParameters
        {
            ConversionCheckPeriodStart = input.ConversionCheckPeriodStart,
            ConversionCheckPeriodEnd = input.ConversionCheckPeriodEnd,
            ItemId = input.ItemId,
            OrderId = input.OrderId
        };
        var request = new KafkaReportRequest(reportRequestParameters)
        {
            UserId = input.UserId,
            RequestParameters = reportRequestParameters
        };

        await userService.RequestReport(input.UserId, request);

        return Ok(new { request.Id });
    }
}