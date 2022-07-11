namespace CheckoutApp.Business.Models;
public record CreateArticleLineRequest
{
    public string Item { get; init; }

    public decimal Price { get; init; }
}
