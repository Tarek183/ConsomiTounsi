using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    [Table("Event")]

    public class Event {
        [Key]
        public int IdEvent { get; set; }
        public string Name { get; set; }
        public string imageE { get; set; }
        public string description { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateD { get; set; }

        [DataType(DataType.Date)]
        public DateTime dateF { get; set; }

        public int  hours { get; set; }
        [DataType(DataType.Date)]

        public DateTime date_publication { get; set; }
        [DataType(DataType.Date)]

        public DateTime deadline{ get; set; }
        public int nbApply { get; set; }
        public int nbVue { get; set; }
        public int IdProduct  { get; set; }
        public int nbRate { get; set; }

    }
}
