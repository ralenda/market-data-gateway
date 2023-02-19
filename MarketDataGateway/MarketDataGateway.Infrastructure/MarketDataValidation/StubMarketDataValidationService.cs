using MarketDataGateway.Model;
using MarketDataGateway.Model.FxQuote;
using MarketDataGateway.Model.MarketDataValidation;

namespace MarketDataGateway.Infrastructure.MarketDataValidation;

/// <summary>
///     A simple stub implementation of an <see cref="IMarketDataValidationService" /> - only validating that a FxQuote
///     if from a list of allowed currency pairs
/// </summary>
public class StubMarketDataValidationService : IMarketDataValidationService
{
    public static readonly string[] ValidCurrencyPairs = { "EUR/USD", "EUR/GBP", "GBP/USD" };

    private readonly HashSet<CurrencyPair> _validCurrencyPairsSet = ValidCurrencyPairs.Select(pair =>
    {
        if (!CurrencyPair.From(pair, out CurrencyPair? currencyPair, out string? reason))
        {
            throw new InvalidOperationException(reason);
        }

        return currencyPair!;
    }).ToHashSet();

    public Task<Model.MarketDataValidation.MarketDataValidation> ValidateAsync(
        MarketDataContributionRequest contribution)
    {
        if (contribution.Quote is FxQuote fxQuote)
        {
            if (_validCurrencyPairsSet.Contains(fxQuote.Symbol))
            {
                return Task.FromResult(
                    Model.MarketDataValidation.MarketDataValidation.Success(MarketDataValidationId.NewId()));
            }

            return Task.FromResult(
                Model.MarketDataValidation.MarketDataValidation.Failure(MarketDataValidationId.NewId(),
                    "The provided currency pair is invalid"));
        }

        return Task.FromResult(Model.MarketDataValidation.MarketDataValidation.Failure(MarketDataValidationId.NewId(),
            $"Unsupported quote type: {contribution.Type}"));
    }
}