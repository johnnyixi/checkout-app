using CheckoutApp.Business.Exceptions;
using CheckoutApp.Business.Services;
using CheckoutApp.Facade;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests.CheckoutApp;

public class BasketFacadeTests
{
    private readonly Mock<IBasketService> _basketRepositoryMock = new();
    private readonly Mock<ILogger<BasketFacade>> _loggerMock = new();

    private readonly BasketFacade _sut;

    public BasketFacadeTests()
    {
        _sut = new BasketFacade(_basketRepositoryMock.Object, _loggerMock.Object);
    }

    // Chose to add this test because it is a little more complex than the other ones.
    [Fact]
    public async Task PayBasket_Logs_BasketAlreadyPayedException_And_Returns_BadRequestObjectResult()
    {
        var basketId = Guid.NewGuid();

        _basketRepositoryMock
            .Setup(repo => repo.PayBasket(basketId))
            .ThrowsAsync(new BasketAlreadyPayedException(basketId));

        _loggerMock
            .Setup(logger => logger.IsEnabled(LogLevel.Error))
            .Returns(true);

        // Act
        var payBasketResponse = await _sut.PayBasket(basketId);

        payBasketResponse.Should().BeOfType<BadRequestObjectResult>();

        var expectedExceptionMessage = $"Basket with Id: [${basketId}] has already been payed.";

        _loggerMock.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == expectedExceptionMessage),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
    }

    // TODO: Add tests for all the methods
}
 