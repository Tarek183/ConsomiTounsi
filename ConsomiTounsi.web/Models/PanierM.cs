using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.Models
{
    public class PanierM {
        public int idPanier { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
    }
}