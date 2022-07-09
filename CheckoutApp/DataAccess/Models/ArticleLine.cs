namespace CheckoutApp.DataAccess.Models;

public class ArticleLine
{
    public int Id { get; set; }
    public string Item { get; set; }

    public decimal Price { get; set; }
}
