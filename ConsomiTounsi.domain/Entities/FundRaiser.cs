using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    public class FundRaiser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FundRaiserID { get; set; }


        [Required]
        [Display(Name = "CIN")]
        public int CIN { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime? BirthDate { get; set; }
        //for now the money will go directly to consumiTounsi 
        // and then they hand it to the fundraiser
        //public int BankAccount { get; set; }


        [Required(ErrorMessage = "Phone Number Required !")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(8)]
        [Display(Name = "Phone Number")]

        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        //the relation has 3 options : [non-profit organisation - relative - self]
        //in  the first two cases ,they need to specify the nature of the relation 
        // example : * relative - a parent 
        //example: * non profit organisation - un enfant un espoir 



        //navigation wishes acts as relationship identifier , 1 to many wishes 
        //for one fundraiser
        //public virtual ICollection<Wish> Wishes { get; set; }

        //navigation kids acts as relationship identifier , 1 to many kids
        //refered by one fundraiser
        public virtual ICollection<Kid> Kids { get; set; }



    }

}
