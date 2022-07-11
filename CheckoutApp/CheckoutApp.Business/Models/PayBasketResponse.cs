namespace CheckoutApp.Business.Models;
public record PayBasketResponse
{
    public Guid BasketId { get; init; }
    public bool Closed { get; init; }

    public bool Payed { get; init; }
}
