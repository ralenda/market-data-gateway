namespace MarketDataGateway.Model;

public interface IMarketDataContributionRepository
{
    public Task<MarketDataContribution> Get(MarketDataContributionId id);

    /// <summary>
    ///     Stores a new contribution returns the corresponding object.
    /// </summary>
    /// <param name="contribution">The contribution to store</param>
    /// <param name="validationResult">The validation result</param>
    /// <returns>The contribution object</returns>
    /// <exception cref="QuoteAlreadyExistException">Thrown if a similar quote already exist in store</exception>
    Task<MarketDataContribution> Store(MarketDataContributionRequest contribution,
        MarketDataValidation.MarketDataValidation validationResult);
}