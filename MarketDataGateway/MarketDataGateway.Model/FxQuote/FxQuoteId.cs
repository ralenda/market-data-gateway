using System.Security.Cryptography;

namespace MarketDataGateway.Model.FxQuote;

/// <summary>
/// Unique identifier of a FxQuote - currency pair + the quote timestamp. This could be extended to include other
/// details such as source, exchange, etc... This can be used as a key to uniquely identify the quote.
/// </summary>
public class FxQuoteId : QuoteId, IEquatable<FxQuoteId>
{
    public bool Equals(FxQuoteId? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Type.Equals(other.Type) && Symbol.Equals(other.Symbol) && Timestamp.Equals(other.Timestamp);
    }

    public override int GetHashCode() => HashCode.Combine(Symbol, Timestamp);

    public CurrencyPair Symbol { get; private init; }

    public DateTimeOffset Timestamp { get; private init; }


    private FxQuoteId() : base(MarketDataType.FxQuote)
    {
    }

    public override bool Equals(QuoteId other) => Equals(other as FxQuoteId);

    public static FxQuoteId From(CurrencyPair currencyPair, DateTimeOffset timestamp)
    {
        return new FxQuoteId
        {
            Symbol = currencyPair,
            Timestamp = timestamp
        };
    }
}