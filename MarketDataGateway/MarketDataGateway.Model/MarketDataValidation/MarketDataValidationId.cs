namespace MarketDataGateway.Model.MarketDataValidation;

public class MarketDataValidationId
{
    public string Value { get; private init; }

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

    /// <summary>
    ///     Creates a new ValidationId (Guid) - normally this should be assigned by the validation service
    ///     but this can be useful for stubbing
    /// </summary>
    public static MarketDataValidationId NewId() => new() { Value = Guid.NewGuid().ToString() };
}