using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    [Table("LigneCommand")]
    public class LigneCommand {
        [Key]
        public int idLigneCommand { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public int quantite { get; set; }
        public int? PanierId { get; set; }
        public int? ProduitId { get; set; }
        public virtual Panier panier { get; set; }
        public virtual Product produit { get; set; }


    }
}
