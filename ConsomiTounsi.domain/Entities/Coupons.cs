using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    [Table("coupon")]
    public class Coupons {
        [Key]
        public int idcoupon { get; set; }
        [DataType(DataType.Date)]
        public DateTime duree { get; set; }
        public enum CategorieP { CategorieP1, CategorieP2, CategorieP3 }
        public float percontage { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public int etat { get; set; }

        public int? panierId { get; set; }
        public virtual Panier panier { get; set; }

    }
}