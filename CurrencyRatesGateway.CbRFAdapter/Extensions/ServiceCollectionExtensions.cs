using System.Text;
using CurrencyRatesGateway.CbRFAdapter.Constants;
using CurrencyRatesGateway.CbRFAdapter.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyRatesGateway.CbRFAdapter.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCbRFAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        var apiUrl = configuration.GetRequiredSection(ConfigPaths.CbRFApiUrl).Get<string>() 
                     ?? throw new NullReferenceException();
        
        services.AddHttpClient(nameof(CbRFAdapter), client => 
        {
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.ParseAdd("text/xml;charset=windows-1251");
        });
        
        services.AddScoped<ICbRFAdapter, CbRFAdapter>();
        
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        return services;
    }
}