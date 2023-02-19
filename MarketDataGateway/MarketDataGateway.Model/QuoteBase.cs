namespace MarketDataGateway.Model;

public abstract class QuoteBase
{
    protected QuoteBase(MarketDataType type) => Type = type;

    public QuoteId Id { get; }

    public MarketDataType Type { get; }
}

public class QuoteId
{
    public string Value { get; }

    protected bool Equals(QuoteId other) => Value == other.Value;

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

        return Equals((QuoteId)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(QuoteId? left, QuoteId? right) => Equals(left, right);

    public static bool operator !=(QuoteId? left, QuoteId? right) => !Equals(left, right);
}