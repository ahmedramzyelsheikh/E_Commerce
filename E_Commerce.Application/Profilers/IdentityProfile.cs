using AutoMapper;
using E_Commerce.Application.DTOs.Identity;
using E_Commerce.Domain.Models.Identity;

namespace E_Commerce.Application.Profilers
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
