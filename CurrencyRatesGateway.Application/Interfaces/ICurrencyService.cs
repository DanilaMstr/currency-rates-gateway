using CurrencyRatesGateway.Domain.Models;

namespace CurrencyRatesGateway.Application.Interfaces;

public interface ICurrencyService
{
    public Task<List<CurrencyRate>> GetCurrencyRatesAsync(string? currencyCode = null, DateTime? date = null);
}