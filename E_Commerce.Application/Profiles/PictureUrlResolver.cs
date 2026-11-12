using AutoMapper;
using AutoMapper.Execution;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Models.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
       public  UrlSettings urlSettings { get; set; }
        public PictureUrlResolver(IOptions<UrlSettings> opt)
        {
            urlSettings = opt.Value;
        }


        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            var baseurl  = urlSettings.BaseURL.TrimEnd('/');
            var pathurl = source.PictureUrl.TrimStart('/');

            return $"{baseurl}/Files/{pathurl}";





        }
    }
    public class UrlSettings
    {
       public string BaseURL { get; set; }
    }

}
