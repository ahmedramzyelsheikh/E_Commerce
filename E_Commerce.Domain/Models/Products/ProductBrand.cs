using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Data.Products
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public ICollection<Product> Products { get; set; }

    }
}
