namespace CheckoutApp.DataAccess.Models;

public class Basket
{
    public Guid Id { get; set; }
    public string Customer { get; set; }

    public decimal TotalNet { get; set; }

    public decimal TotalGross { get; set; }

    public bool PaysVAT { get; set; }

    public List<ArticleLine> Items { get; set; }

    public bool Closed { get; set; }

    public bool Payed { get; set; }
}
