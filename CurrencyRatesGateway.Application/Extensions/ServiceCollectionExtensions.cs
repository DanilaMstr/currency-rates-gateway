using CurrencyRatesGateway.Application.Interfaces;
using CurrencyRatesGateway.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyRatesGateway.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrencyService, CurrencyService>();
        
        return services;
    }
}