using E_Commerce.Domain.Data.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.Configurations
{
    public class ConfigurationProduct : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                   .WithMany(p => p.Products)
                   .HasForeignKey(p => p.BrandId);


            builder.HasOne(p => p.ProductType)
                   .WithMany(p => p.Products)
                   .HasForeignKey(p => p.TypeId);

            builder.Property(p => p.Name)
                   .HasMaxLength(100);

            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            builder.Property(p => p.PictureUrl)
                   .HasMaxLength(200);

            builder.Property(p => p.Price)
                  .HasColumnType("decimal(18,2)"); 


        }
    }
}
