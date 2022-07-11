using AutoMapper;
using CheckoutApp.Business.Models;
using CheckoutApp.DataAccess.Models;

namespace CheckoutApp.Business.Profiles;

internal class CheckoutProfile : Profile
{
    public CheckoutProfile()
    {
        CreateMap<ArticleLine, ArticleLineResponse>();
        CreateMap<Basket, BasketResponse>();
    }
}
