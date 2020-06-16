using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.Models
{
    public class Productt
    {
        public int IdProduct { get; set; }
        public string Nom { get; set; }
        public string imageP { get; set; }
        public enum CategorieP { CategorieP1, CategorieP2, CategorieP3 };
        public long barcode { get; set; }
        public string marque { get; set; }
        public double price { get; set; }
        public int? IdRayon { get; set; }

        public virtual Rayonn rayon { get; set; }
        public DateTime dateC { get; set; }
        public DateTime dateF { get; set; }
    }
}