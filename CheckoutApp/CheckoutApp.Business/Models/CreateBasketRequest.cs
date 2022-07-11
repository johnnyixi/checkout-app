namespace CheckoutApp.Business.Models;

[Serializable]
public record CreateBasketRequest
{
    public string Customer { get; init; }
    public bool PaysVAT { get; init; }
}
