namespace MarketDataGateway.Model;

public interface IMarketDataContributionRepository
{
    public Task<MarketDataContribution> Get(MarketDataContributionId id);

    Task<MarketDataContribution> Store(MarketDataContributionRequest contribution,
        MarketDataValidation.MarketDataValidation validationResult);
}