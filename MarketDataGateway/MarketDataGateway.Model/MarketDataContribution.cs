namespace MarketDataGateway.Model;

public class MarketDataContribution
{
    public MarketDataContributionId Id { get; internal set; }

    public MarketDataType Type => Quote.Type;

    public MarketDataValidation.MarketDataValidation ValidationStatus { get; internal set; }

    public QuoteBase Quote { get; internal set; }

    public static MarketDataContribution From(MarketDataContributionId id, MarketDataContributionRequest request,
        MarketDataValidation.MarketDataValidation validation) =>
        new()
        {
            Id = id,
            ValidationStatus = validation,
            Quote = request.Quote
        };
}