using CurrencyRatesGateway.Domain.Models;

namespace CurrencyRatesGateway.CbRFAdapter.Interfaces;

public interface ICbRFAdapter
{
    public Task<List<CurrencyRate>> GetCurrencyRatesAsync(DateTime date);
}