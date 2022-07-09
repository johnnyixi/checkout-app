using CheckoutApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckoutApp.DataAccess;

public class CheckoutContext : DbContext
{
    public CheckoutContext(DbContextOptions options) : base(options) { }

    public DbSet<Basket> Basket { get; set; }
    public DbSet<ArticleLine> ArticleLines { get; set; }
}
