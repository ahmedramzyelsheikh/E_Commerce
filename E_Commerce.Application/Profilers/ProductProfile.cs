using AutoMapper;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profilers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {

            //  create mapping  product ->  productDto  and  make some configration because there is different   names ; 
            CreateMap<Product, ProductDTO>()
                .ForMember(dist => dist.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.ProductType, opt => opt.MapFrom(src => src.ProductType.Name));

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();  
                 
           
               
                 
        }

    }
}
