namespace CheckoutApp.Business.Models;

[Serializable]
public class BasketResponse
{
    public string Customer { get; init; }
    public bool PaysVAT { get; init; }
    public decimal TotalNet { get; set; }
    public decimal TotalGross { get; set; }
    public bool Closed { get; init; }
    public bool Payed { get; init; }
    public IReadOnlyCollection<ArticleLineResponse> Items { get; init; }

}