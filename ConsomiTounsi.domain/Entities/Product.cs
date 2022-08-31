﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    public enum CategorieP {
        Desserts, Poissons, Cafés, Champignons, sucres,
        Sésame, Gingembre, BoissonsGazeuses, Pousses, Crabes,
        Snacks, Fruits, bananes, Epicerie, Surgelés, Graines, Légumes
    };
    [Table("Product")]

    public class Product {
        [Key]
        public int IdProduct { get; set; }
        public string Nom { get; set; }
        public string imageP { get; set; }
        public string description { get; set; }
        public CategorieP CategorieP { get; set; }
        public string barcode { get; set; }
        public string marque { get; set; }
        public double price { get; set; }
        public int? IdRayon { get; set; }
        [ForeignKey("IdRayon")]
        public virtual Rayon rayon { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateC { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateF { get; set; }
        public int qte { get; set; }

        public virtual ICollection<LigneCommand> LigneCommands { get; set; }




    }
}