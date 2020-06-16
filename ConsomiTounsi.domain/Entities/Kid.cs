using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    public enum Referer { Relative, Nonprofit };
    public class Kid
    {

        public Kid()
        {
            //adding a random image to the kid profile
            //kid[1,4].png
            Random rdm = new Random();
            int nbr = rdm.Next(1, 4);
            ImagePath = "~\\images\\kids\\kid" + nbr + ".png";

        }
        public int KidID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string KidFistName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string KidSecondName { get; set; }

        //TODO: must be btw [2 --> 18] today
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime KidBirthdate { get; set; }


        //the disease or disability
        [Required]
        [Display(Name = "Kid Diagnosis")]
        public string KidDiagnosis { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Medical Professional Email ")]
        public string MedicalProfessionalEmail { get; set; }


        public string ImagePath { get; set; }
        public int Age { get; set; }

        public string Name
        {

            get { return KidFistName + " " + KidSecondName; }
        }

        [Required]
        [Display(Name = "Relationship to child")]
        public Referer RelationshipToChild { get; set; }


        [Display(Name = "Nature of the relation")]
        public string RelationSpecification { get; set; }



        [Display(Name = "Organization name")]
        public string OrganizationName { get; set; }



        [ForeignKey("FundRaiser")]
        public int FundRaiserID { get; set; }

        //[ForeignKey("Wish")]
        //public int WishID { get; set; }
        //navigation Fundraiser acts as relationship identifier , 
        //a kid is refered by one fundraiser
        public virtual FundRaiser FundRaiser { get; set; }


        //public virtual Wish Wish { get; set; }


    }

}
