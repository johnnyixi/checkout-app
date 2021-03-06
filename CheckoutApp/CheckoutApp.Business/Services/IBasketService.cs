using CheckoutApp.Business.Models;

namespace CheckoutApp.Business.Services;

public interface IBasketService
{
    /// <summary>
    /// Adds a new basket.
    /// </summary>
    /// <param name="customer">The customer name.</param>
    /// <param name="paysVAT">Flag that indicates if the customer pays VAT.</param>
    /// <returns>The Id of the newly </returns>
    Task<Guid> AddBasketAsync(string customer, bool paysVAT);

    /// <summary>
    /// Retrieves the information related to a basket.
    /// </summary>
    /// <param name="basketId"></param>
    /// <returns></returns>
    Task<BasketResponse?> GetBasketAsync(Guid basketId);

    /// <summary>
    /// Adds a new Article to a basket if the basket is found.
    /// </summary>
    /// <param name="basketId">The id of the basket on which we want to add the new Article.</param>
    /// <param name="itemName">The name of the new Article.</param>
    /// <param name="itemPrice">The price of the new Article.</param>
    /// <returns></returns>
    Task<CreateArticleLineResponse?> AddArticleLineToBasketAsync(Guid basketId, string itemName, decimal itemPrice);

    /// <summary>
    /// Marks the basket as being payed and closed.
    /// </summary>
    /// <param name="basketId">The id of the basket to be paid and closed.</param>
    /// <returns>A payed object or null if the basket was not found.</returns>
    /// <exception cref="T:BasketAlreadyPayedException">The collection was modified after the enumerator was created. </exception>
    Task<PayBasketResponse?> PayBasket(Guid basketId);
}
