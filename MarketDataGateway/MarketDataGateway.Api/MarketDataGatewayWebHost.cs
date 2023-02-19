using Microsoft.AspNetCore;

namespace MarketDataGateway.Api;

public static class MarketDataGatewayWebHost
{
    public static IWebHostBuilder CreateDefaultBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .ConfigureKestrel((builderContext, options) =>
            {
                var config = builderContext.Configuration.GetSection("Kestrel");
                options.Configure(config);
            })
            .UseStartup<Startup>();
    }
}