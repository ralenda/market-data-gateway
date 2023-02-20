namespace MarketDataGateway.Model.FxQuote;

/// <summary>
///     Simple Value type object representing a FxQuote
/// </summary>
public class FxQuote : QuoteBase
{
    public FxQuote(CurrencyPair currencyPair, DateTimeOffset timestamp) : base(FxQuoteId.From(currencyPair, timestamp))
    {
    }

    public CurrencyPair Symbol => ((FxQuoteId)Id).Symbol;

    public decimal Bid { get; }

    public decimal Ask { get; }

    public DateTimeOffset Timestamp => ((FxQuoteId)Id).Timestamp;
}