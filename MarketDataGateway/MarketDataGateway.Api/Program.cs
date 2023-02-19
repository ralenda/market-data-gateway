using MarketDataGateway.Api;
using Microsoft.AspNetCore;

IWebHost host = MarketDataGatewayWebHost.CreateDefaultBuilder(args)
    .Build();

host.Run();