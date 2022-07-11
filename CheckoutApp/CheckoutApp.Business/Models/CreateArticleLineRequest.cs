namespace CheckoutApp.Business.Models;

[Serializable]
public record CreateArticleLineRequest
{
    public string Item { get; init; }

    public decimal Price { get; init; }
}
