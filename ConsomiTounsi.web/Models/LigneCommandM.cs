using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.Models
{
    public class LigneCommandM {
        public int idLigneCommand { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public int quantite { get; set; }

        public ProductM produit { get; set; }

    }
}