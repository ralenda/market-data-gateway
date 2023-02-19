namespace MarketDataGateway.Model.MarketDataValidation;

public class MarketDataValidation
{
    public MarketDataValidationId Id { get; private set; }

    public MarketDataValidationResult Result { get; private set; }

    public string? Reason { get; internal set; }

    public MarketDataValidation Success(MarketDataValidationId validationId) =>
        new MarketDataValidation
        {
            Id = validationId,
            Result = MarketDataValidationResult.Valid
        };

    public MarketDataValidation Failure(MarketDataValidationId validationId, string reason) =>
        new MarketDataValidation
        {
            Id = validationId,
            Result = MarketDataValidationResult.Invalid,
            Reason = reason
        };
}