namespace MarketDataGateway.Model.FxQuote;

/// <summary>
///     Simple Value type object representing a FxQuote
/// </summary>
public class FxQuote : QuoteBase
{
    private FxQuote(CurrencyPair currencyPair, DateTimeOffset timestamp) : base(FxQuoteId.From(currencyPair, timestamp))
    {
    }

    public CurrencyPair Symbol => ((FxQuoteId)Id).Symbol;

    public decimal Bid { get; private init; }

    public decimal Ask { get; private init; }

    public DateTimeOffset Timestamp => ((FxQuoteId)Id).Timestamp;

    public static FxQuote From(CurrencyPair currencyPair, DateTimeOffset timestamp, decimal bid, decimal ask)
    {
        return new FxQuote(currencyPair, timestamp) { Bid = bid, Ask = ask };
    }
}