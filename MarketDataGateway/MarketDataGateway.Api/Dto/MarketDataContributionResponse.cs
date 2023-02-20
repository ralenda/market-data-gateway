using MarketDataGateway.Model;

namespace MarketDataGateway.Api.Dto;

public class MarketDataContributionResponse
{
    public string Id { get; set; }

    public static MarketDataContributionResponse FromModel(MarketDataContribution contribution) =>
        // Running out of time but this would contain much more fields
        new MarketDataContributionResponse
        {
            Id = contribution.Id.Value
        };
}