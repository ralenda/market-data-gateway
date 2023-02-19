namespace MarketDataGateway.Model.MarketDataValidation;

public class MarketDataValidationId
{
    public string Value { get; }

    protected bool Equals(MarketDataValidationId other) => Value == other.Value;

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

        return Equals((MarketDataValidationId)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(MarketDataValidationId? left, MarketDataValidationId? right) => Equals(left, right);

    public static bool operator !=(MarketDataValidationId? left, MarketDataValidationId? right) => !Equals(left, right);
}