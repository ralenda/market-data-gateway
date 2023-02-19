using MarketDataGateway.Model.MarketDataValidation;

namespace MarketDataGateway.Model.Service;

/// <summary>
///     Domain service
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

    public async Task<MarketDataContribution> AddContribution(MarketDataContributionRequest request)
    {
        // we probably want to check whether the quote already exist in some way (e.g., same timestamp and symbol,
        // may be different depending on asset class. But due to lack of time and to not overcomplexify things I will
        // not do that now

        MarketDataValidation.MarketDataValidation validationResult = await _validationService.ValidateAsync(request)
            .ConfigureAwait(false);
        if (validationResult.Result == MarketDataValidationResult.Invalid)
        {
            return MarketDataContribution.From(request, validationResult);
        }

        return await _contributionRepository.Store(request, validationResult).ConfigureAwait(false);
    }
}