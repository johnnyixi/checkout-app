using CheckoutApp.DataAccess;
using CheckoutApp.DataAccess.Exceptions;
using CheckoutApp.DataAccess.Models;
using CheckoutApp.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.CheckoutApp.DataAccess;

public class BasketRepositoryTests
{
    private const string CustomerName = nameof(CustomerName);

    private readonly DbContextOptions<CheckoutContext> _dbContextOptions;
    private readonly BasketRepository _sut;


    public BasketRepositoryTests()
    {
        var dbName = $"CheckoutAppDb_{DateTime.Now.ToFileTimeUtc()}";

        _dbContextOptions = new DbContextOptionsBuilder<CheckoutContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        _sut = CreateRepository();
    }

    [Fact]
    public async Task GetBasket_Returns_Null_When_Basket_Not_Found()
    {
        var basketId = Guid.NewGuid();

        // Act
        var basket = await _sut.GetBasketAsync(basketId);

        // Assert
        basket.Should().BeNull();
    }

    [Fact]
    public async Task GetBasket_Returns_Basket_When_Basket_Was_Found()
    {

        var basketEntity = CreateDefaultBasket();

        await _sut.AddAsync(basketEntity);

        // Act
        var basket = await _sut.GetBasketAsync(basketEntity.Id);

        // Assert
        basket.Should().NotBeNull();
        basket.Should().BeEquivalentTo(basketEntity);
    }

    [Fact]
    public async Task AddAsync_Throws_EntityNullRepositoryException_When_Basket_Is_Null()
    {
        Basket? nullBasket = null;

        // Act
        Func<Task> act = async () => { await _sut.AddAsync(nullBasket); };

        // Assert
        await act.Should().ThrowAsync<EntityNullRepositoryException>();
    }

    [Fact]
    public async Task AddAsync_Does_Not_Throw_When_Add_Was_Successful()
    {
        var basketEntity = CreateDefaultBasket();

        // Act
        await _sut.AddAsync(basketEntity);

        // Assert
        var basket = await _sut.GetBasketAsync(basketEntity.Id);

        basket.Should().NotBeNull();
        basket.Should().BeEquivalentTo(basketEntity);
    }

    [Fact]
    public async Task AddAsync_Returns_Throws_AddAsyncRepositoryException_On_SaveChanges_Exception()
    {
        var basketEntity = CreateDefaultBasket();

        await _sut.AddAsync(basketEntity);

        // Act
        Func<Task> act = async () => { await _sut.AddAsync(basketEntity); };

        // Assert
        await act.Should().ThrowAsync<AddAsyncRepositoryException>();
    }

    [Fact]
    public async Task UpdateAsync_Throws_EntityNullRepositoryException_When_Basket_Is_Null()
    {
        Basket? nullBasket = null;

        // Act
        Func<Task> act = async () => { await _sut.UpdateAsync(nullBasket); };

        // Assert
        await act.Should().ThrowAsync<EntityNullRepositoryException>();
    }

    [Fact]
    public async Task UpdateAsync__Does_Not_Throw_When_Update_Was_Successful()
    {
        var basketEntity = CreateDefaultBasket();

        await _sut.AddAsync(basketEntity);

        var basket = await _sut.GetAsync(basketEntity.Id);

        basket.Payed = true;
        basket.Closed = true;

        // Act
        await _sut.UpdateAsync(basketEntity);

        var updatedBasket = await _sut.GetAsync(basketEntity.Id);

        // Assert
        updatedBasket.Should().NotBeNull();
        updatedBasket?.Payed.Should().BeTrue();
        updatedBasket?.Closed.Should().BeTrue();
    }


    private BasketRepository CreateRepository()
    {
        var context = new CheckoutContext(_dbContextOptions);
       
        return new BasketRepository(context);
    }

    private static Basket CreateDefaultBasket()
        => new()
        {
            Customer = CustomerName,
            PaysVAT = false
        };
}