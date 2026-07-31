using AutoMapper;
using E_Commerce.API.Common;
using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Application.Profilers;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Specification;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Implemetion
{
    public class ProductServices(IUnitOfWork unitofwork, IMapper _mapping) : IProductService
    {
        public async Task<Results<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct =default)
        {
            var brands = await unitofwork.GetRepositries<ProductBrand, int>().GetAll(ct);
            var   brandDTo  =  _mapping.Map<IReadOnlyList<BrandDto>>(brands);

            return Results<IReadOnlyList<BrandDto>>.OK(brandDTo);  
           
        }

        public async Task<Results<IReadOnlyList<ProductDTO>>> GetAllProductsAsync(QueryParams queryParams ,CancellationToken ct = default)
        {
            var productspec = new ProductSpecification(queryParams); 
            var products = await unitofwork.GetRepositries<Product, int>().GetAll(productspec, ct);
            var productdtos =  _mapping.Map<IReadOnlyList<ProductDTO>>(products);

            return Results<IReadOnlyList<ProductDTO>>.OK(productdtos); 
        }

        public async Task<Results<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct)
        {
            var producttype = await unitofwork.GetRepositries<ProductType , int>().GetAll(ct);
            var producttypedto = _mapping.Map<IReadOnlyList<TypeDto>>(producttype);

            return Results<IReadOnlyList<TypeDto>>.OK(producttypedto);  

        }

        public async Task<Results<ProductDTO?>> GetByIdAsync(int id, CancellationToken ct)

        {
            var  productSpecfication =  new ProductSpecification(id);
            var product = await unitofwork.GetRepositries<Product , int>().GetById(productSpecfication ,ct);

            if (product == null)
                return Results<ProductDTO>.Fail(Errors.NotFound("Product.NotFound"));

            var productdto = _mapping.Map<ProductDTO>(product);  

            return Results<ProductDTO>.OK(productdto); 

        }
    }
}
