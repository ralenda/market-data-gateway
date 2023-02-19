using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MarketDataGateway.Api.Dto;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

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
        var request = new MarketDataContributionRequest();

        HttpResponseMessage response = await _testClient!.PostAsJsonAsync("/market-data", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }
}