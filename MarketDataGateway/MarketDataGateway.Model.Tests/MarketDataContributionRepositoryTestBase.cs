using MarketDataGateway.Model.FxQuote;
using MarketDataGateway.Model.MarketDataValidation;
using NUnit.Framework;

namespace MarketDataGateway.Model.Tests;

/// <summary>
///     Base test class to test generic repository logic - we can then instanciate that with different repository
///     implementation
/// </summary>
[TestFixture]
public abstract class MarketDataContributionRepositoryTestBase
{
    protected abstract IMarketDataContributionRepository GetRepository();

    [Test]
    public async Task Stores_Multiple_Identical_FxQuotes_Throws()
    {
        DateTimeOffset timestamp = DateTimeOffset.UtcNow;
        CurrencyPair.From("Eur/USD", out CurrencyPair? pair, out string? _);
        MarketDataContributionRequest request1 =
            MarketDataContributionRequest.FxContributionRequest(pair!, 1, 1, timestamp);
        MarketDataContributionRequest request2 =
            MarketDataContributionRequest.FxContributionRequest(pair!, 1, 1, timestamp);

        MarketDataValidation.MarketDataValidation validationResult1 =
            MarketDataValidation.MarketDataValidation.Success(MarketDataValidationId.NewId());
        MarketDataValidation.MarketDataValidation validationResult2 =
            MarketDataValidation.MarketDataValidation.Success(MarketDataValidationId.NewId());

        IMarketDataContributionRepository repository = GetRepository();
        Assert.DoesNotThrowAsync(() => repository.Store(request1, validationResult1));
        Assert.ThrowsAsync<QuoteAlreadyExistException>(() => repository.Store(request2, validationResult2));
    }
}