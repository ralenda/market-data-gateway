using MarketDataGateway.Model.FxQuote;

namespace MarketDataGateway.Model;

public class MarketDataContributionRequest
{
    public MarketDataType Type => Quote.Type;

    public QuoteBase Quote { get; private init; }

    public static MarketDataContributionRequest FxContributionRequest(CurrencyPair currencyPair, decimal bid,
        decimal ask, DateTimeOffset timestamp)
    {
        return new MarketDataContributionRequest { Quote = FxQuote.FxQuote.From(currencyPair, timestamp, bid, ask) };
    }
}