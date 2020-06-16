using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.data.Configurations
{
    class PanierConfiguration : EntityTypeConfiguration<Panier> {
        public PanierConfiguration()
        {
            //HasRequired<LigneCommand>()

        }
    }
}
