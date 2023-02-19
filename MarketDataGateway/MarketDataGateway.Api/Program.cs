using MarketDataGateway.Api;

IWebHost host = MarketDataGatewayWebHost.CreateDefaultBuilder(args)
    .Build();

host.Run();