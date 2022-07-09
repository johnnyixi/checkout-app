using CheckoutApp.DataAccess.Interfaces;
using CheckoutApp.DataAccess.Models;

namespace CheckoutApp.DataAccess.Repositories;

internal class ArticleLineRepository : Repository<ArticleLine>, IArticleLineRepository
{
    public ArticleLineRepository(CheckoutContext context) : base(context) { }
}
