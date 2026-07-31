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

            if (!string.IsNullOrEmpty(queryParams.Sort))
            {
                switch (queryParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(n => n.Name);
            }

            ApplyPaging(queryParams.PageSize * (queryParams.PageIndex - 1), queryParams.PageSize);
        }
        public ProductSpecification(int id  ) :  base(p=> p.Id==id)
        {
            Adding(p => p.ProductBrand);
            Adding(p => p.ProductType);
        }

    }
}
