using System.Net;
using MarketDataGateway.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MarketDataGateway.Api.Controllers;

[ApiController]
[Route("market-data")]
public class MarketDataController : ControllerBase
{
    [HttpPost]
    public ActionResult<MarketDataContributionResponse> Contribute(MarketDataContributionRequest request)
    {
        return StatusCode((int) HttpStatusCode.NotImplemented);
    }
}