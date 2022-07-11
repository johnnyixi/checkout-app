using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CheckoutApp.DataAccess.Models;

public class ArticleLine
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public Guid BasketId { get; set; }

    [JsonIgnore]
    public virtual Basket Basket { get; set; }

    public string Item { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public ArticleLine()
    {
        Id = Guid.NewGuid();
    }
}
