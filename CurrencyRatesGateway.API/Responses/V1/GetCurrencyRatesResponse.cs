
using Swashbuckle.AspNetCore.Annotations;

namespace CurrencyRatesGateway.API.Responses.V1;

[SwaggerSchema(Title = "Ответ со списком валют", Description = "Содержит список курсов валют", Nullable = false)]
public class GetCurrencyRatesResponse
{
    [SwaggerSchema("Список курсов валют", Format = "array", Nullable = false)]
    public List<CurrencyRate> CurrencyRates { get; set; }
    
    [SwaggerSchema(Title = "Курс валюты", Nullable = false)]
    public class CurrencyRate
    {
        [SwaggerSchema(Title = "Внутренний уникальный код валют", Nullable = false)]
        public string Id { get; init; } = null!;
    
        [SwaggerSchema(Title = "ISO Цифр. код валюты", Nullable = false)]
        public ushort NumCode { get; init; }

        [SwaggerSchema(Title = "ISO Букв. код валюты", Nullable = false)]
        public string CharCode { get; init; } = null!;
    
        [SwaggerSchema(Title = "Номинал. ед", Nullable = false)]
        public uint Nominal { get; init; }
    
        [SwaggerSchema(Title = "Название валюты", Nullable = false)]
        public string Name { get; init; } = null!;
    
        [SwaggerSchema(Title = "Значение", Nullable = false)]
        public string Value { get; init; } = null!;
    
        [SwaggerSchema(Title = "Курс за 1 единицу валюты", Nullable = false)]
        public string VunitRate { get; init; } = null!;
    }
}