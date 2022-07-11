namespace CheckoutApp.Business.Models;
public record ArticleLineResponse
{
    public string Item { get; init; }
    public decimal Price { get; init; }
}
