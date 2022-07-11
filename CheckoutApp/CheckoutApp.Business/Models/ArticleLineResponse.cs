namespace CheckoutApp.Business.Models;

[Serializable]
public record ArticleLineResponse
{
    public string Item { get; init; }
    public decimal Price { get; init; }
}
