using E_Commerce.API.Common;
using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Contracts
{
    public interface IProductService
    {
        Task<Results<IReadOnlyList<ProductDTO>>> GetAllProductsAsync(QueryParams queryParams, CancellationToken ct = default); 

        Task<Results<ProductDTO?>> GetByIdAsync(int id  , CancellationToken ct);


        Task<Results<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct);

        Task<Results<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct);

    }
}
