using CheckoutApp.Business.Exceptions;
using CheckoutApp.Business.Models;
using CheckoutApp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using IdempotentAPI.Filters;

namespace CheckoutApp.Controllers;

public class BasketsController : BaseController
{
    private readonly IBasketService _basketService;

    public BasketsController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateBasketResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBasket(Guid id)
    {
        var basket = await _basketService.GetBasketAsync(id);

        if (basket is null)
        {
            return NotFound($"The basket with Id:[{id}] was not found.");
        }

        return Ok(basket);
    }

    [HttpPost]
    [Idempotent(Enabled = true)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateBasketResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateBasket([FromHeader(Name = "IdempotencyKey")] Guid idempotencyKey, CreateBasketRequest createBasketRequest)
    {
        var basketId = await _basketService.AddBasketAsync(createBasketRequest.Customer, createBasketRequest.PaysVAT);

        return Created($"/Baskets/{basketId}",new CreateBasketResponse
        {
            BasketId = basketId
        });
    }

    [HttpPut("{id}/article-line")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddArticleLineToBasket(Guid id, CreateArticleLineRequest articleLineRequest)
    {
        var basket = await _basketService.AddArticleLineToBasketAsync(id, articleLineRequest.Item, articleLineRequest.Price);

        if (basket == null)
        {
            return BadRequest($"The basket with Id:[{id}] was not found.");
        }

        return Ok(basket);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PayBasketResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PayBasket(Guid id)
    {
        PayBasketResponse? payBasketResponse;

        try
        {
            payBasketResponse = await _basketService.PayBasket(id);
        }
        catch (BasketAlreadyPayedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest("bla");
        }

        if (payBasketResponse is null)
        {
            return BadRequest($"The basket with Id:[{id}] was not found.");
        }

        return Ok(payBasketResponse);
    }

}
