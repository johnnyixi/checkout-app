using CheckoutApp.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutApp.Facade;
public interface IBasketFacade
{
    Task<IActionResult> GetBasket(Guid id);

    Task<IActionResult> CreateBasket(CreateBasketRequest createBasketRequest);

    Task<IActionResult> AddArticleLineToBasket(Guid id, CreateArticleLineRequest articleLineRequest);

    Task<IActionResult> PayBasket(Guid id);
}
