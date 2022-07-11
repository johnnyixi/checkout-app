using System.ComponentModel.DataAnnotations.Schema;

namespace CheckoutApp.DataAccess.Models;

public class Basket
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    public string Customer { get; set; }

    public bool PaysVAT { get; set; }

    public ICollection<ArticleLine> Items { get; set; }

    public bool Closed { get; set; }

    public bool Payed { get; set; }

    public Basket()
    {
        Id = Guid.NewGuid();
        Items = new List<ArticleLine>();
    }
}
