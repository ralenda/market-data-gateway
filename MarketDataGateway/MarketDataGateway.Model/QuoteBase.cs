namespace MarketDataGateway.Model;

public abstract class QuoteBase
{
    protected QuoteBase(QuoteId quoteId) => Id = quoteId;

    public QuoteId Id { get; }

    public MarketDataType Type => Id.Type;
}

public abstract class QuoteId : IEquatable<QuoteId>
{
    public MarketDataType Type { get; }

    public override int GetHashCode()
    {
        return (int)Type;
    }

    protected QuoteId(MarketDataType type)
    {
        Type = type;
    }

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

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((QuoteId)obj);
    }

    public static bool operator ==(QuoteId? left, QuoteId? right) => Equals(left, right);

    public static bool operator !=(QuoteId? left, QuoteId? right) => !Equals(left, right);
    public abstract bool Equals(QuoteId? other);
}