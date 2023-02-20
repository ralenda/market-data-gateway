using MarketDataGateway.Infrastructure.MarketDataContributionRepository;
using MarketDataGateway.Infrastructure.MarketDataValidation;
using MarketDataGateway.Model;
using MarketDataGateway.Model.MarketDataValidation;
using MarketDataGateway.Model.Service;

namespace MarketDataGateway.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Register Application services
        services.AddSingleton<IMarketDataContributionRepository, InMemoryMarketDataContributionRepository>();
        services.AddSingleton<IMarketDataValidationService, StubMarketDataValidationService>();
        services.AddScoped<MarketDataContributionService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseEndpoints(builder => builder.MapControllers());
    }
}