using System.ComponentModel;

namespace CurrencyRatesGateway.Domain.Models;

[Description("Курс валюты")]
public class CurrencyRate
{
    [Description("Внутренний уникальный код валют")]
    public string Id { get; set; } = null!;
    
    [Description("ISO Цифр. код валюты")]
    public ushort NumCode { get; set; }

    [Description("ISO Букв. код валюты")]
    public string CharCode { get; set; } = null!;
    
    [Description("Номинал. ед")]
    public uint Nominal { get; set; }
    
    [Description("Название валюты")]
    public string Name { get; set; } = null!;
    
    [Description("Значение")]
    public string Value { get; set; } = null!;
    
    [Description("Курс за 1 единицу валюты")]
    public string VunitRate { get; set; } = null!;
}