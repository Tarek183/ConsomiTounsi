using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.Models
{
    public class Rayonn
    {
        [Key]
        public int IdRayon { get; set; }
        public enum typeR { type1, type2, type3 }
        public string imageR { get; set; }

        public virtual ICollection<Productt> products { get; set; }
    }
}