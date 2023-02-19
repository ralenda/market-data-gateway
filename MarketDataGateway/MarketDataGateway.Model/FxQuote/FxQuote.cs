namespace MarketDataGateway.Model.FxQuote;

/// <summary>
///     Simple Value type object representing a FxQuote
/// </summary>
public class FxQuote : QuoteBase
{
    public FxQuote() : base(MarketDataType.FxQuote)
    {
    }

    public CurrencyPair Symbol { get; }

    public decimal Bid { get; }

    public decimal Ask { get; }

    public DateTimeOffset Timestamp { get; }
}