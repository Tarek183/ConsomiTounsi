using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.data.Configurations
{
    class CouponConfiguration : EntityTypeConfiguration<Coupons> {
        public CouponConfiguration()
        {
            HasOptional<Panier>(c => c.panier)
                 .WithMany(p => p.coupons)
                 .HasForeignKey<int?>(c => c.panierId);


        }
    }
}
