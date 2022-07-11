namespace CheckoutApp.Business.Models;

[Serializable]
public record CreateArticleLineResponse : ArticleLineResponse
{
    public Guid Id { get; init; }
}
