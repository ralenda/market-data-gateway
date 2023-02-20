using MarketDataGateway.Api.Dto;
using MarketDataGateway.Model;
using MarketDataGateway.Model.Service;
using Microsoft.AspNetCore.Mvc;
using MarketDataContributionRequest = MarketDataGateway.Api.Dto.MarketDataContributionRequest;

namespace MarketDataGateway.Api.Controllers;

[ApiController]
[Route("market-data")]
public class MarketDataController : ControllerBase
{
    private readonly MarketDataContributionService _service;

    public MarketDataController(MarketDataContributionService service) => _service = service;

    // We should add some swagger attriutes here, such as expected status code and response format
    [HttpPost]
    public async Task<ActionResult<MarketDataContributionResponse>> Contribute(
        [FromBody] MarketDataContributionRequest request)
    {
        if (request.ToModel(out Model.MarketDataContributionRequest? modelRequest, out string? reason))
        {
            try
            {
                MarketDataContribution result = await _service.AddContribution(modelRequest!);
                // I am not implementing Get due to lack of time but otherwise we should specify the URI
                return Created("/", MarketDataContributionResponse.FromModel(result));
            }
            catch (QuoteAlreadyExistException)
            {
                // We want to return a problemDetails here but running out of time
                return BadRequest();
            }
        }

        // Ideally I will want a problemDetails here but right now that will do
        return BadRequest(reason);
    }
}