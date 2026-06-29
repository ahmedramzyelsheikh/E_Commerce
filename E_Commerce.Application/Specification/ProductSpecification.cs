using E_Commerce.Application.Common;
using E_Commerce.Domain.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specification
{
    public class ProductSpecification : BaseSpecification<Product, int>
    {
        public ProductSpecification(QueryParams queryParams) : base(
            p => (!queryParams.BrandId.HasValue || queryParams.BrandId ==  p.BrandId) &&
                  (!queryParams.TypeId.HasValue || queryParams.TypeId == p.TypeId)     &&
                  (string.IsNullOrEmpty(queryParams.ByName) || p.Name.ToLower().Contains(queryParams.ByName.ToLower())))
        {
            Adding(p => p.ProductBrand);
            Adding(p => p.ProductType);
        }
        public ProductSpecification(int id  ) :  base(p=> p.Id==id)
        {
            Adding(p => p.ProductBrand);
            Adding(p => p.ProductType);
        }

    }
}
