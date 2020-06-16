using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    //public enum typeR { Laitiers, Maison, Bebe, Bio, Animaux, Surgeles, Marche, Hygiene, Boissons, EpicerieSalee, EpicerieSucree, PtitDej };

    public class Rayon {
        [Key]
        public int IdRayon { get; set; }
        public string description { get; set; }
        public string typeR { get; set; }
        public string imageR { get; set; }
        public string flag { get; set; }

        public virtual IEnumerable<Product> products { get; set; }
    }
}
