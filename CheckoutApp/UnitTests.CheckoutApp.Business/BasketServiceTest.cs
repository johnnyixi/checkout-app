using AutoMapper;
using CheckoutApp.Business.Exceptions;
using CheckoutApp.Business.Models;
using CheckoutApp.Business.Profiles;
using CheckoutApp.Business.Services;
using CheckoutApp.DataAccess.Exceptions;
using CheckoutApp.DataAccess.Interfaces;
using CheckoutApp.DataAccess.Models;
using FluentAssertions;

namespace UnitTests.CheckoutApp.Business;

public class BasketServiceTest
{
    private const string CustomerName = nameof(CustomerName);
    private const decimal DefaultVatRate = 1.1m;

    private readonly Mock<IBasketRepository> _basketRepositoryMock = new();
    private readonly Mock<IVatService> _vatServiceMock = new();
    private readonly IMapper _mapper;

    private readonly BasketService _sut;

    public BasketServiceTest()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CheckoutProfile>());

        _mapper = mapperConfiguration.CreateMapper();

        _vatServiceMock
            .Setup(vatService => vatService.GetDefaultVatRate())
            .Returns(DefaultVatRate);

        _sut = new BasketService(_basketRepositoryMock.Object, _vatServiceMock.Object, _mapper);
    }

    [Theory]
    [InlineData("12345", true)]
    [InlineData("John@ Doe", false)]
    [InlineData("     ab", true)]
    public async Task AddBasketAsync_Throws_InvalidCustomerNameException_With_Invalid_Data(string customer,
        bool paysVat)
    {
        // Act
        Func<Task> act = async () => { await _sut.AddBasketAsync(customer, paysVat); };

        // Assert
        await act.Should().ThrowAsync<InvalidCustomerNameException>();
    }

    [Fact]
    public async Task AddBasketAsync_Forwards_Exceptions_Thrown_By_Repository()
    {
        _basketRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Basket>()))
            .ThrowsAsync(new AddAsyncRepositoryException(new Exception()));

        // Act
        Func<Task> act = async () => { await _sut.AddBasketAsync("customer", false); };

        // Assert
        await act.Should().ThrowAsync<AddAsyncRepositoryException>();
    }

    [Theory]
    [InlineData("Johnny", true)]
    [InlineData("John Doe", false)]
    [InlineData("The John", true)]
    public async Task AddBasketAsync_Returns_BasketId_With_Valid_Data(string customer, bool paysVat)
    {
        // Act
        var basketId = await _sut.AddBasketAsync(customer, paysVat);

        // Assert
        basketId.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetBasketAsync_Returns_Null_When_Basket_Not_Found()
    {
        var randomBasketId = Guid.NewGuid();

        // Act
        var basket = await _sut.GetBasketAsync(randomBasketId);

        // Assert
        basket.Should().BeNull();
    }

    [Fact]
    public async Task GetBasketAsync_Returns_Simple_BasketResponse()
    {
        var initialBasket = CreateDefaultBasket();

        _basketRepositoryMock
            .Setup(repo => repo.GetBasketAsync(initialBasket.Id))
            .ReturnsAsync(initialBasket);

        // Act
        var basket = await _sut.GetBasketAsync(initialBasket.Id);

        // Assert
        basket.Should().NotBeNull();

        var expectedBasketResponse = _mapper.Map<Basket, BasketResponse>(initialBasket);

        basket.Should().BeEquivalentTo(expectedBasketResponse);
        basket.Items.Should().BeEmpty();
        basket.TotalNet.Should().Be(0);
        basket.TotalGross.Should().Be(0);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GetBasketAsync_Returns_Complex_BasketResponse(bool paysVat)
    {
        var initialBasket = CreateComplexBasket(paysVat);

        _basketRepositoryMock
            .Setup(repo => repo.GetBasketAsync(initialBasket.Id))
            .ReturnsAsync(initialBasket);

        // Act
        var basket = await _sut.GetBasketAsync(initialBasket.Id);

        // Assert
        basket.Should().NotBeNull();
        basket.Customer.Should().Be(initialBasket.Customer);
        basket.Items.Should().HaveCount(initialBasket.Items.Count);
        basket.TotalNet.Should().Be(initialBasket.Items.Sum(item => item.Price));

        var expectedGross = basket.TotalNet * (paysVat ? DefaultVatRate : 1.0m);

        basket.TotalGross.Should().Be(expectedGross);
    }

    // TODO: Unit tests for AddArticleLineToBasketAsync
    // TODO: Unit tests for PayBasket

    private static Basket CreateDefaultBasket(bool paysVat = false)
        => new()
        {
            Customer = CustomerName,
            PaysVAT = paysVat
        };

    private static Basket CreateComplexBasket(bool paysVat = false)
    {
        return new Basket
        {
            Customer = CustomerName,
            PaysVAT = paysVat,
            Items = new List<ArticleLine>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Item = "Item1",
                    Price = 1.10m
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Item = "Item2",
                    Price = 3.10m
                }
            }
        };
    }
}