namespace MarketDataGateway.Model;

/// <summary>
///     Identifier of a MarketDataContribution
/// </summary>
public class MarketDataContributionId
{
    public string Value { get; private init; }
    protected bool Equals(MarketDataContributionId other) => Value == other.Value;

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

        return Equals((MarketDataContributionId)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(MarketDataContributionId? left, MarketDataContributionId? right) =>
        Equals(left, right);

    public static bool operator !=(MarketDataContributionId? left, MarketDataContributionId? right) =>
        !Equals(left, right);

    public static MarketDataContributionId NewId()
    {
        return new MarketDataContributionId { Value = Guid.NewGuid().ToString() };
    }
}