using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MarketDataGateway.Model;
using MarketDataGateway.Model.FxQuote;

namespace MarketDataGateway.Api.Dto;

public class MarketDataContributionRequest
{
    public FxQuoteRequest? FxQuoteRequest;

    // TODO: as far as I remember aspnet does not validate enums, it may require a custom validator
    [Required] public MarketDataType Type;

    // Need a better validation system but that will do for now
    public bool ToModel(out Model.MarketDataContributionRequest? modelRequest, out string? reason)
    {
        if (Type == MarketDataType.FxQuote)
        {
            if (FxQuoteRequest != null)
            {
                return FxQuoteRequest.ToModel(out modelRequest, out reason);
            }

            modelRequest = null;
            reason = "FxQuoteRequest must be specified";
            return false;
        }

        modelRequest = null;
        reason = "Unsupported MarketDataType";
        return false;
    }
}

public class FxQuoteRequest
{
    [DataMember] public string Symbol { get; set; }

    [DataMember] public decimal Bid { get; set; }

    [DataMember] public decimal Ask { get; set; }

    [DataMember] public DateTimeOffset Timestamp { get; set; }

    public bool ToModel(out Model.MarketDataContributionRequest? contribution, out string? reason)
    {
        if (CurrencyPair.From(Symbol, out CurrencyPair? pair, out reason))
        {
            contribution = Model.MarketDataContributionRequest.FxContributionRequest(pair!, Bid, Ask, Timestamp);
            return true;
        }

        contribution = null;
        return false;
    }
}