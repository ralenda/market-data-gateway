namespace MarketDataGateway.Model.FxQuote;

public class CurrencyPair
{
    public string Value { get; private init; }

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

    public static bool From(string symbol, out CurrencyPair? currencyPair, out string? reason)
    {
        // validation should go there
        currencyPair = new CurrencyPair { Value = symbol };
        reason = null;
        return true;
    }

    public override string ToString() => Value;
}