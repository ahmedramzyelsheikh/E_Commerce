using AutoMapper;
using AutoMapper.Execution;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Data.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profilers
{
    public class PictureURLResolve : IValueResolver<Product, ProductDTO, string>
    {
       public  UrlSetteings urlSetteings { get; set; }
        public PictureURLResolve(IOptions<UrlSetteings> opt)
        {
            urlSetteings = opt.Value;
        }


        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            var baseurl  = urlSetteings.BaseURL.TrimEnd('/');
            var pathurl = source.PictureUrl.TrimStart('/');

            return $"{baseurl}/Files/{pathurl}";





        }
    }
    public class UrlSetteings
    {
       public string BaseURL { get; set; }
    }

}
