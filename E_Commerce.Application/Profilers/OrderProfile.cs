using AutoMapper;
using E_Commerce.Application.DTOs.OrderModule;
using E_Commerce.Domain.Models.OrderModule;

namespace E_Commerce.Application.Profilers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Address, E_Commerce.Application.DTOs.Identity.AddressDto>().ReverseMap();
            
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod!.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod!.Cost))
                .ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal()))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl));
        }
    }
}
