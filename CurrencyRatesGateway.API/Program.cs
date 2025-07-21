using CurrencyRatesGateway.Application.Extensions;
using CurrencyRatesGateway.CbRFAdapter.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Currency Rates Gateway API", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.AddApplicationServices();
builder.Services.AddCbRFAdapter(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency Rates Gateway API v1"));

app.UseHttpsRedirection();
app.MapControllers();

app.Run();