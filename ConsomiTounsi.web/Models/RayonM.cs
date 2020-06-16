using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.Models
{
    //public enum typeR { Laitiers, Maison, Bebe, Bio, Animaux, Surgeles, Marche, Hygiene, Boissons, EpicerieSalee, EpicerieSucree, PtitDej };

    public class RayonM {
        [Key]
        public int IdRayon { get; set; }
        public string description { get; set; }
        public string typeR { get; set; }
        public string imageR { get; set; }
        public string flag { get; set; }
        public virtual IEnumerable<ProductM> products { get; set; }
    }
    }