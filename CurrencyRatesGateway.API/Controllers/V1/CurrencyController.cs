using CurrencyRatesGateway.API.Responses.V1;
using CurrencyRatesGateway.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CurrencyRatesGateway.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение информации из сайта Банка России по курсам валют",
        Description = "Возвращает список курсов валют")]
    [SwaggerResponse(200, "Successfully retrieved currency rates")]
    [SwaggerResponse(204, "No content for specified currency code")]
    public async Task<ActionResult<GetCurrencyRatesResponse>> GetCurrencyRatesAsync(
        [FromQuery, SwaggerSchema(Title = "ISO Букв. код валюты")] string? currencyCode = null,
        [FromQuery, SwaggerSchema(Title = "Дата курса")] DateTime? date = null)
    {
        var result = await _currencyService.GetCurrencyRatesAsync(currencyCode, date);

        if (result.Count == 0)
        {
            return NoContent();
        }
        
        return Ok(result);
    }
}