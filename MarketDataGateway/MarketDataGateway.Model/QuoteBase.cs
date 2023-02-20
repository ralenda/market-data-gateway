namespace MarketDataGateway.Model;

public abstract class QuoteBase
{
    protected QuoteBase(QuoteId quoteId) => Id = quoteId;

    public QuoteId Id { get; }

    public MarketDataType Type => Id.Type;
}

public abstract class QuoteId : IEquatable<QuoteId>
{
    protected QuoteId(MarketDataType type) => Type = type;

    public MarketDataType Type { get; }
    public abstract bool Equals(QuoteId? other);

    public override int GetHashCode() => (int)Type;

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

    public static bool operator ==(QuoteId? left, QuoteId? right) => Equals(left, right);

    public static bool operator !=(QuoteId? left, QuoteId? right) => !Equals(left, right);
}