using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.data.Configurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product> {
        public ProductConfiguration()
        {
            //Rayon 0..1       * Film
            HasOptional(p => p.rayon)
                .WithMany(r => (ICollection<Product>)r.products)
                .HasForeignKey(p => p.IdRayon)
                .WillCascadeOnDelete(true);
        }
    }
}