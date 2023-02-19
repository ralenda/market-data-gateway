namespace MarketDataGateway.Model.MarketDataValidation;

public interface IMarketDataValidationService
{
    public Task<MarketDataValidation> ValidateAsync(MarketDataContributionRequest contribution);
}