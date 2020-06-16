using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    public class Donation
    {
        public int ID { get; set; }
        public double Sum { get; set; }
        public DateTime Date { get; set; }



        //the donor
        public virtual User User { get; set; }
        public string UserId { get; set; }

        //the wish 

        public virtual Wish Wish { get; set; }

        public int WishId { get; set; }

    }
}
