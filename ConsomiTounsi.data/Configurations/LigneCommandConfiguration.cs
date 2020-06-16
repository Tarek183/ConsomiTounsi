using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.data.Configurations
{
    class LigneCommandConfiguration : EntityTypeConfiguration<LigneCommand> {

        public LigneCommandConfiguration()
        {
            /*
             HasRequired(l => l.panier)
                 .WithMany(p => (ICollection<LigneCommand>)p.LigneCommands)
                 .HasForeignKey(l => l.idLigneCommand)
                 .WillCascadeOnDelete(true);
             HasRequired(l => l.produit)
                 .WithMany(p => (ICollection<LigneCommand>)p.LigneCommands)
                 .HasForeignKey(l => l.idLigneCommand)
                 .WillCascadeOnDelete(true);
                 */
            HasRequired<Panier>(l => l.panier)
            .WithMany(p => p.LigneCommands)
             .HasForeignKey<int?>(l => l.PanierId);
            HasRequired<Product>(l => l.produit)
                .WithMany(p => p.LigneCommands)
                .HasForeignKey<int?>(l => l.ProduitId);



        }
    }
}