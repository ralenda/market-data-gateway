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

public class CurrencyPair
{
    public string Value { get; }

    protected bool Equals(CurrencyPair other) => Value == other.Value;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((CurrencyPair)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(CurrencyPair? left, CurrencyPair? right) => Equals(left, right);

    public static bool operator !=(CurrencyPair? left, CurrencyPair? right) => !Equals(left, right);
}