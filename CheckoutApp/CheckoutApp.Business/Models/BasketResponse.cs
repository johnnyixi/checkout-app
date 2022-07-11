namespace CheckoutApp.Business.Models;

public class BasketResponse
{
    public string Customer { get; set; }
    public bool PaysVAT { get; set; }
    public decimal TotalNet => Items.Sum(item => item.Price);
    public decimal TotalGross { get; set; }
    public bool Closed { get; set; }
    public bool Payed { get; set; }
    public IReadOnlyCollection<ArticleLineResponse> Items { get; init; }

}