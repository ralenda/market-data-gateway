using MarketDataGateway.Model.FxQuote;
using MarketDataGateway.Model.MarketDataValidation;
using MarketDataGateway.Model.Service;
using Moq;
using NUnit.Framework;

namespace MarketDataGateway.Model.Tests;

public class MarketDataContributionServiceTests
{
    [Test]
    public async Task Validation_Is_Successful_Stores_Quote()
    {
        // Arrange
        CurrencyPair.From("Eur/USD", out var pair, out var _);
        var request = MarketDataContributionRequest.FxContributionRequest(pair!, 0, 0, DateTimeOffset.UtcNow);
        
        var validationResult = MarketDataValidation.MarketDataValidation.Success(MarketDataValidationId.NewId());
        var quoteValidationMock = new Mock<IMarketDataValidationService>();
        quoteValidationMock.Setup(mock => mock.ValidateAsync(request))
            .ReturnsAsync(validationResult);
        
        var contribution = MarketDataContribution.From(MarketDataContributionId.NewId(),
            request, validationResult);

        var quoteStore = new Mock<IMarketDataContributionRepository>();
        quoteStore.Setup(mock => mock.Store(request, validationResult)).ReturnsAsync(contribution);
        
        // When the service is called
        var service = new MarketDataContributionService(quoteValidationMock.Object, quoteStore.Object);
        var result = await service.AddContribution(request);

        Assert.That(result, Is.EqualTo(contribution));
        
        quoteValidationMock.Verify(mock => mock.ValidateAsync(request), Times.Once);
        quoteStore.Verify(mock => mock.Store(request, validationResult), Times.Once);
        quoteValidationMock.VerifyNoOtherCalls();
        quoteStore.VerifyNoOtherCalls();
    }
    
    [Test]
    public async Task Validation_Fails_Does_Not_Store_Quote()
    {
        // Arrange
        CurrencyPair.From("Eur/USD", out var pair, out var _);
        var request = MarketDataContributionRequest.FxContributionRequest(pair!, 0, 0, DateTimeOffset.UtcNow);
        
        var validationResult = MarketDataValidation.MarketDataValidation.Failure(MarketDataValidationId.NewId(), "invalid");
        var quoteValidationMock = new Mock<IMarketDataValidationService>();
        quoteValidationMock.Setup(mock => mock.ValidateAsync(request))
            .ReturnsAsync(validationResult);

        var quoteStore = new Mock<IMarketDataContributionRepository>();
        
        // When the service is called
        var service = new MarketDataContributionService(quoteValidationMock.Object, quoteStore.Object);
        var result = await service.AddContribution(request);

        Assert.That(result.ValidationStatus.Result, Is.EqualTo(MarketDataValidationResult.Invalid));
        
        quoteValidationMock.Verify(mock => mock.ValidateAsync(request), Times.Once);
        quoteStore.Verify(mock => mock.Store(request, validationResult), Times.Never);
        quoteValidationMock.VerifyNoOtherCalls();
        quoteStore.VerifyNoOtherCalls();
    }
}