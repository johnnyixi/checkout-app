using CheckoutApp.Business.Models;
using CheckoutApp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using IdempotentAPI.Filters;

namespace CheckoutApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDetails))]
    public class BasketsController : ControllerBase
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
                return NotFound();
            }

            return Ok(basket);
        }

        [HttpPost]
        [Idempotent(Enabled = true)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateBasketResponse))]
        public async Task<IActionResult> CreateBasket([FromHeader(Name = "IdempotencyKey")] Guid idempotencyKey, CreateBasketRequest createBasketRequest)
        {
            var basketId = await _basketService.AddBasketAsync(createBasketRequest.Customer, createBasketRequest.PaysVAT);

            return Created($"/Baskets/{basketId}",new CreateBasketResponse
            {
                BasketId = basketId
            });
        }

        [HttpPut("{id}/article-line")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddArticleLineToBasket(Guid id, CreateArticleLineRequest articleLineRequest)
        {
            var basket = await _basketService.AddArticleLineToBasketAsync(id, articleLineRequest.Item, articleLineRequest.Price);
            return Ok(basket);
        }

        [HttpPatch("{id}")]
        [Idempotent(Enabled = true)]
        public async Task<IActionResult> PayBasket(Guid id)
        {
            await _basketService.PayBasket(id);

            return Ok();
        }

    }
}
