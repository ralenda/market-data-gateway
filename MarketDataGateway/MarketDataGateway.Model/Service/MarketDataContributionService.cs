using MarketDataGateway.Model.MarketDataValidation;

namespace MarketDataGateway.Model.Service;

/// <summary>
///     Domain service - logic to validate a contribution and store it if successful
/// </summary>
public class MarketDataContributionService
{
    private readonly IMarketDataContributionRepository _contributionRepository;
    private readonly IMarketDataValidationService _validationService;

    public MarketDataContributionService(IMarketDataValidationService validationService,
        IMarketDataContributionRepository contributionRepository)
    {
        _validationService = validationService;
        _contributionRepository = contributionRepository;
    }

    /// <summary>
    ///     Validate a contribution and stores it if it is valid
    /// </summary>
    /// <param name="request">The contribution to validate and store</param>
    /// <returns>The contribution object</returns>
    /// <exception cref="QuoteAlreadyExistException">Thrown if a similar quote already exist in store</exception>
    public async Task<MarketDataContribution> AddContribution(MarketDataContributionRequest request)
    {
        // we probably want to check whether the quote already exist in some way (e.g., same timestamp and symbol,
        // may be different depending on asset class. But due to lack of time and to not overcomplexify things I will
        // not do that now

        MarketDataValidation.MarketDataValidation validationResult = await _validationService.ValidateAsync(request)
            .ConfigureAwait(false);
        if (validationResult.Result == MarketDataValidationResult.Invalid)
        {
            return MarketDataContribution.From(MarketDataContributionId.NewId(), request, validationResult);
        }

        return await _contributionRepository.Store(request, validationResult).ConfigureAwait(false);
    }
}