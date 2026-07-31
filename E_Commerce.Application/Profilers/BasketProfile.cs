using AutoMapper;
using E_Commerce.Application.DTOs.Basket;
using E_Commerce.Domain.Models.Basket;

namespace E_Commerce.Application.Profilers
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
