using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public  class QueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? ByName { get; set; }
    }
}
