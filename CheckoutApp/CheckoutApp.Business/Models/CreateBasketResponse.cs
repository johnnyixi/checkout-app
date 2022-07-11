namespace CheckoutApp.Business.Models;

[Serializable]
public record CreateBasketResponse
{
    public Guid BasketId { get; init; }
}
