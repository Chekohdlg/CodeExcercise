using CodeExercise_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercise.DataAcccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Category).WithMany(x => x.Products);
            builder.HasData(GetData());
        }

        private List<Product> GetData()
        {
            return new List<Product>()
            {
                new Product() { Id=1, AgeRestriction=2, Company="Mattel",Description="A new toy from mattel",Name="rex", Price=255, ImageUrl= "https://http2.mlstatic.com/D_NQ_NP_799752-MLM44066560930_112020-O.webp" },
                new Product() { Id=2, AgeRestriction=5, Company="Disney",Description="A new toy from disney",Name="Princess", Price=300, ImageUrl= "https://http2.mlstatic.com/D_NQ_NP_799752-MLM44066560930_112020-O.webp" },
            };
        }
    }
}
