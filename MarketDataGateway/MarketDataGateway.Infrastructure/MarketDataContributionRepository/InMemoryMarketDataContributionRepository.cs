using System.Collections.Concurrent;
using MarketDataGateway.Model;

namespace MarketDataGateway.Infrastructure.MarketDataContributionRepository;

public class InMemoryMarketDataContributionRepository : IMarketDataContributionRepository
{
    // We want to validate whether a similar quote already exist - we keep track of contribution indexed by their
    // quote identifier
    private readonly ConcurrentDictionary<QuoteId, MarketDataContribution> _quoteStore = new();
    // This is used to retrieve q contribution by its contributionId
    private readonly ConcurrentDictionary<MarketDataContributionId, MarketDataContribution> _contributionStore = new();


    public Task<MarketDataContribution> Get(MarketDataContributionId id) => throw new NotImplementedException();

    public Task<MarketDataContribution> Store(MarketDataContributionRequest contribution,
        Model.MarketDataValidation.MarketDataValidation validationResult)
    {
        var toStore = MarketDataContribution.From(MarketDataContributionId.NewId(), contribution, validationResult);
        // Check if a contribution for the same quote already exist
        // As we do not support delete or update, no complex locking is required to update both maps
        if (!_quoteStore.TryAdd(toStore.Quote.Id, toStore))
        {
            throw new QuoteAlreadyExistException();
        }

        // This should always succeed (assuming no Id collision)
        _contributionStore.TryAdd(toStore.Id, toStore);

        return Task.FromResult(toStore);


    }
}