using CurrencyRatesGateway.Application.Interfaces;
using CurrencyRatesGateway.CbRFAdapter.Interfaces;
using CurrencyRatesGateway.Domain.Models;

namespace CurrencyRatesGateway.Application.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICbRFAdapter _cbRFAdapter;
    
    public CurrencyService(ICbRFAdapter cbRFAdapter)
    {
        _cbRFAdapter = cbRFAdapter;
    }
    
    public async Task<List<CurrencyRate>> GetCurrencyRatesAsync(string? currencyCode = null, DateTime? date = null)
    {
        var currencyRates = await _cbRFAdapter.GetCurrencyRatesAsync(date ?? DateTime.Now);

        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            return currencyRates;
        }
        
        return currencyRates
            .Where(x => string.Equals(x.CharCode, currencyCode, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}