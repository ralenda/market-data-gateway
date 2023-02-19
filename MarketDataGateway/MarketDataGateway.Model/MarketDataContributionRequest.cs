namespace MarketDataGateway.Model;

public class MarketDataContributionRequest
{
    public MarketDataType Type => Quote.Type;

    public QuoteBase Quote { get; }
}