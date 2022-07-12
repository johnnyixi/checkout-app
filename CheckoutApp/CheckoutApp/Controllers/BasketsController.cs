using CheckoutApp.Business.Models;
using CheckoutApp.Facade;
using Microsoft.AspNetCore.Mvc;
using IdempotentAPI.Filters;

namespace CheckoutApp.Controllers;

public class BasketsController : BaseController
{
    private readonly IBasketFacade _basketFacade;

    public BasketsController(IBasketFacade basketFacade)
    {
        _basketFacade = basketFacade ?? throw new ArgumentNullException(nameof(basketFacade));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateBasketResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> GetBasket(Guid id)
    {
        return await _basketFacade.GetBasket(id);
    }

    [HttpPost]
    [Idempotent(Enabled = true)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateBasketResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateBasket([FromHeader(Name = "IdempotencyKey")] Guid idempotencyKey, CreateBasketRequest createBasketRequest)
    {
        return await _basketFacade.CreateBasket(createBasketRequest);
    }

    [HttpPut("{id}/article-line")]
    [ProducesResponseType(typeof(ArticleLineResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddArticleLineToBasket(Guid id, CreateArticleLineRequest articleLineRequest)
    {
        return await _basketFacade.AddArticleLineToBasket(id, articleLineRequest);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PayBasketResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> PayBasket(Guid id)
    {
        return await _basketFacade.PayBasket(id);
    }
}
