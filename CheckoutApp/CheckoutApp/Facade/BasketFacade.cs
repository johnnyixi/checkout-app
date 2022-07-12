using CheckoutApp.Business.Exceptions;
using CheckoutApp.Business.Models;
using CheckoutApp.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutApp.Facade;

public class BasketFacade : IBasketFacade
{
    private readonly IBasketService _basketService;
    private readonly ILogger<BasketFacade> _logger;

    public BasketFacade(IBasketService basketService, ILogger<BasketFacade> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
    }

    public async Task<IActionResult> GetBasket(Guid id)
    {

        var basket = await _basketService.GetBasketAsync(id);

        if (basket is null)
        {
            return new NotFoundObjectResult(BasketNotFoundErrorDetails(id));
        }

        return new OkObjectResult(basket);
    }

    public async Task<IActionResult> CreateBasket(CreateBasketRequest createBasketRequest)
    {
        var basketId = await _basketService.AddBasketAsync(createBasketRequest.Customer, createBasketRequest.PaysVAT);

        return new CreatedResult($"/Baskets/{basketId}", new CreateBasketResponse
        {
            BasketId = basketId
        });
    }

    public async Task<IActionResult> AddArticleLineToBasket(Guid id, CreateArticleLineRequest articleLineRequest)
    {
        var basket = await _basketService.AddArticleLineToBasketAsync(id, articleLineRequest.Item, articleLineRequest.Price);

        if (basket == null)
        {
            return new BadRequestObjectResult(BasketNotFoundErrorDetails(id));
        }

        return new OkObjectResult(basket);
    }

    public async Task<IActionResult> PayBasket(Guid id)
    {
        PayBasketResponse? payBasketResponse;

        try
        {
            payBasketResponse = await _basketService.PayBasket(id);
        }
        catch (BasketAlreadyPayedException ex)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(ex, ex.Message);
            }

            return new BadRequestObjectResult(ex.Message);
        }

        if (payBasketResponse is null)
        {
            return new BadRequestObjectResult(BasketNotFoundErrorDetails(id));
        }

        return new OkObjectResult(payBasketResponse);
    }

    private static ErrorDetails BasketNotFoundErrorDetails(Guid id) 
        => new($"The basket with Id:[{id}] was not found.");
}

