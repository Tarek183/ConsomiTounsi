using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    [Table("panier")]
    public class Panier {
        [Key]
        public int idPanier { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public int ClientId { get; set; }
        public int Validation { get; set; }
        public virtual ICollection<Coupons> coupons { get; set; }
        public virtual ICollection<LigneCommand> LigneCommands { get; set; }

    }
}