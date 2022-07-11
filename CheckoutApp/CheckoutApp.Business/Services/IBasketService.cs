using CheckoutApp.Business.Models;

namespace CheckoutApp.Business.Services;

public interface IBasketService
{
    Task<Guid> AddBasketAsync(string customer, bool paysVAT);
    Task<BasketResponse?> GetBasketAsync(Guid basketId);

    Task<CreateArticleLineResponse?> AddArticleLineToBasketAsync(Guid basketId, string itemName, decimal itemPrice);

    Task PayBasket(Guid basketId);
}
