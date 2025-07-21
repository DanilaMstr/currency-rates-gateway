using CurrencyRatesGateway.Application.Services;
using CurrencyRatesGateway.CbRFAdapter.Interfaces;
using CurrencyRatesGateway.Domain.Models;
using Moq;

namespace CurrencyRatesGateway.Application.Tests;

[TestClass]
public class CurrencyServiceTests 
{ 
    private Mock<ICbRFAdapter> _cbRFAdapterMock; 
    private CurrencyService _currencyService;

    [TestInitialize]
    public void Initialize()
    {
        _cbRFAdapterMock = new Mock<ICbRFAdapter>();
        _currencyService = new CurrencyService(_cbRFAdapterMock.Object);
    }

    [TestMethod]
    public async Task GetCurrencyRatesAsync_NoParameters_ReturnsAllRates()
    {
        // Arrange
        var expectedRates = new List<CurrencyRate>
        { 
            new CurrencyRate { CharCode = "USD", Value = "75.5" }, 
            new CurrencyRate { CharCode = "EUR", Value = "85.0" }
        };

        _cbRFAdapterMock.Setup(x => x.GetCurrencyRatesAsync(It.IsAny<DateTime>()))
        .ReturnsAsync(expectedRates);

        // Act
        var result = await _currencyService.GetCurrencyRatesAsync();

        // Assert
        Assert.AreEqual(expectedRates.Count, result.Count);
        CollectionAssert.AreEqual(expectedRates, result);
        
        _cbRFAdapterMock.Verify(x => x.GetCurrencyRatesAsync(It.IsAny<DateTime>()), Times.Once);
    }

    [TestMethod]
    public async Task GetCurrencyRatesAsync_WithCurrencyCode_ReturnsFilteredRates()
    {
        // Arrange
        var currencyCode = "USD";
        var allRates = new List<CurrencyRate>
        { 
            new CurrencyRate { CharCode = "USD", Value = "75.5" }, 
            new CurrencyRate { CharCode = "EUR", Value = "85.0" }
        };

        _cbRFAdapterMock.Setup(x => x.GetCurrencyRatesAsync(It.IsAny<DateTime>()))
        .ReturnsAsync(allRates);

        // Act
        var result = await _currencyService.GetCurrencyRatesAsync(currencyCode);

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(currencyCode, result[0].CharCode);
        
        _cbRFAdapterMock.Verify(x => x.GetCurrencyRatesAsync(It.IsAny<DateTime>()), Times.Once);
    }

    [TestMethod]
    public async Task GetCurrencyRatesAsync_WithNonexistentCurrencyCode_ReturnsEmptyList()
    {
        // Arrange
        var currencyCode = "ABC";
        var allRates = new List<CurrencyRate>
        {
            new CurrencyRate { CharCode = "USD", Value = "75.5" }, 
            new CurrencyRate { CharCode = "EUR", Value = "85.0" }
        };

        _cbRFAdapterMock.Setup(x => x.GetCurrencyRatesAsync(It.IsAny<DateTime>()))
        .ReturnsAsync(allRates);

        // Act
        var result = await _currencyService.GetCurrencyRatesAsync(currencyCode);

        // Assert
        Assert.AreEqual(0, result.Count);
        _cbRFAdapterMock.Verify(x => x.GetCurrencyRatesAsync(It.IsAny<DateTime>()), Times.Once);
    }

    [TestMethod]
    public async Task GetCurrencyRatesAsync_CurrencyCodeCaseInsensitive_ReturnsCorrectRate()
    {
        // Arrange
        var currencyCode = "usd";
        var allRates = new List<CurrencyRate>
        {
            new CurrencyRate { CharCode = "USD", Value = "75.5" }, 
            new CurrencyRate { CharCode = "EUR", Value = "85.0" }
        };

        _cbRFAdapterMock.Setup(x => x.GetCurrencyRatesAsync(It.IsAny<DateTime>()))
        .ReturnsAsync(allRates);

        // Act
        var result = await _currencyService.GetCurrencyRatesAsync(currencyCode);

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("USD", result[0].CharCode);
    }
}