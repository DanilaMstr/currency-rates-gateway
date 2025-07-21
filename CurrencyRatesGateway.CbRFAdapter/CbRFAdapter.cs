using System.Text;
using System.Xml.Serialization;
using CurrencyRatesGateway.CbRFAdapter.Interfaces;
using CurrencyRatesGateway.CbRFAdapter.Models;
using CurrencyRatesGateway.Domain.Models;
using Microsoft.Extensions.Logging;

namespace CurrencyRatesGateway.CbRFAdapter;

internal class CbRFAdapter : ICbRFAdapter
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CbRFAdapter> _logger;

    public CbRFAdapter(IHttpClientFactory httpClientFactory, ILogger<CbRFAdapter> logger)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(CbRFAdapter));
        _logger = logger;
    }

    public async Task<List<CurrencyRate>> GetCurrencyRatesAsync(DateTime date)
    {
        var requestUri = $"scripts/XML_daily.asp?date_req={date:dd/MM/yyyy}";

        var response = await ExecuteRequestOrDefault(requestUri, new ValCurs());
        
        return response.Valute.Select(x => new CurrencyRate
        {
            Id = x.Id,
            NumCode = x.NumCode,
            CharCode = x.CharCode,
            Nominal = x.Nominal,
            Name = x.Name,
            Value = x.Value,
            VunitRate = x.VunitRate
        }).ToList();
    }
    
    private async Task<T> ExecuteRequestOrDefault<T>(string requestUri, T defaultValue) where T : class
    { 
        try 
        {
            using var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream, Encoding.GetEncoding(1251));
            
            var serializer = new XmlSerializer(typeof(T));
            
            if (serializer.Deserialize(reader) is not T result)
            {
                return defaultValue;
            }
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{ErrorMessage} from CB RF. URI: {RequestUri}", ex.Message, requestUri);
            
            return defaultValue;
        }
    }
}