using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MarketDataGateway.Api.Dto;
using MarketDataGateway.Model;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using MarketDataContributionRequest = MarketDataGateway.Api.Dto.MarketDataContributionRequest;

namespace MarketDataGateway.Api.Tests.Acceptance;

/// <summary>
///     Base acceptance tests for the Market Data Gateway web api
/// </summary>
public class MarketDataContributionAcceptanceTests
{
    private HttpClient? _testClient;
    private TestServer? _testServer;

    [SetUp]
    public void Setup()
    {
        _testServer = new TestServer(
            MarketDataGatewayWebHost.CreateDefaultBuilder(Array.Empty<string>()));
        _testClient = _testServer.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _testClient?.Dispose();
        _testServer?.Dispose();
    }

    [Test]
    public async Task Contribute_Valid_MarketDataRequest_Successful_Response()
    {
        var request = new MarketDataContributionRequest
        {
            Type = MarketDataType.FxQuote,
            FxQuoteRequest = new FxQuoteRequest
            {
                Ask = 0,
                Bid = 0,
                Symbol = "EUR/USD",
                Timestamp = DateTimeOffset.UtcNow
            }
        };

        // This test is not working - apparently there is a deserialisation problem which I don;t have time to debug
        // Probably something stupid
        string? serialised = JsonConvert.SerializeObject(request);
        HttpResponseMessage response = await _testClient!.PostAsync("/market-data",
            new StringContent(serialised, Encoding.UTF8, "application/json"));

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }
}