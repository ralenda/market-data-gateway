using MarketDataGateway.Model.FxQuote;
using MarketDataGateway.Model.MarketDataValidation;
using NUnit.Framework;

namespace MarketDataGateway.Model.Tests;

/// <summary>
/// Base test class to test generic repository logic - we can then instanciate that with different repository implementation
/// </summary>
[TestFixture]
public abstract class MarketDataContributionRepositoryTestBase
{
    protected abstract IMarketDataContributionRepository GetRepository();

    [Test]
    public async Task Stores_Multiple_Identical_FxQuotes_Throws()
    {
        var timestamp = DateTimeOffset.UtcNow;
        CurrencyPair.From("Eur/USD", out var pair, out var _);
        var request1 = MarketDataContributionRequest.FxContributionRequest(pair!, 1, 1, timestamp);
        var request2 = MarketDataContributionRequest.FxContributionRequest(pair!, 1, 1, timestamp);

        var validationResult1 = MarketDataValidation.MarketDataValidation.Success(MarketDataValidationId.NewId());
        var validationResult2 = MarketDataValidation.MarketDataValidation.Success(MarketDataValidationId.NewId());

        var repository = GetRepository();
        Assert.DoesNotThrowAsync(() => repository.Store(request1, validationResult1));
        Assert.ThrowsAsync<QuoteAlreadyExistException>(() => repository.Store(request2, validationResult2));
    }
}