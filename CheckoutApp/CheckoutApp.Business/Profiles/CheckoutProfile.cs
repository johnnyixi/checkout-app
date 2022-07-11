using AutoMapper;
using CheckoutApp.Business.Models;
using CheckoutApp.DataAccess.Models;

namespace CheckoutApp.Business.Profiles;

public class CheckoutProfile : Profile
{
    public CheckoutProfile()
    {
        CreateMap<ArticleLine, ArticleLineResponse>();
        CreateMap<ArticleLine, CreateArticleLineResponse>();
        CreateMap<Basket, BasketResponse>();
    }
}
