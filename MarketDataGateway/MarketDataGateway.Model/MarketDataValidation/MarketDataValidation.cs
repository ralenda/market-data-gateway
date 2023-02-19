namespace MarketDataGateway.Model.MarketDataValidation;

public class MarketDataValidation
{
    public MarketDataValidationId Id { get; private set; }

    public MarketDataValidationResult Result { get; private set; }

    public string? Reason { get; internal set; }

    public static MarketDataValidation Success(MarketDataValidationId validationId) =>
        new()
        {
            Id = validationId,
            Result = MarketDataValidationResult.Valid
        };

    public static MarketDataValidation Failure(MarketDataValidationId validationId, string reason) =>
        new()
        {
            Id = validationId,
            Result = MarketDataValidationResult.Invalid,
            Reason = reason
        };
}