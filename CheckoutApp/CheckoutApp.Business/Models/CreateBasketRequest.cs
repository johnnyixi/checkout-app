namespace CheckoutApp.Business.Models;
public record CreateBasketRequest
{
    public string Customer { get; init; }
    public bool PaysVAT { get; init; }
}
