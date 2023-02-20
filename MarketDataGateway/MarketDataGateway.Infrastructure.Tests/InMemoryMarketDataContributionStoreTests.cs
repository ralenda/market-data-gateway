using MarketDataGateway.Infrastructure.MarketDataContributionRepository;
using MarketDataGateway.Model;
using MarketDataGateway.Model.Tests;
using NUnit.Framework;

namespace MarketDataGateway.Infrastructure.Tests;

[TestFixture]
public class InMemoryMarketDataContributionStoreTests : MarketDataContributionRepositoryTestBase
{
    protected override IMarketDataContributionRepository GetRepository() => new InMemoryMarketDataContributionRepository();
}