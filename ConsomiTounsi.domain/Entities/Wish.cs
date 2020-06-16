using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    public class Wish
    {


        public Wish()
        {
            // FundRaised initiated to 0
            //simulate the donation 
            Random rdm = new Random();
            int nbr = rdm.Next(50, 200);
            FundRaised = nbr;

            // the start date initiated  to today 
            StartDate = DateTime.Now;


        }
        public int WishID { get; set; }



        [Required]
        [MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$",
        ErrorMessage = "Numbers and special characters are not allowed in the wish title")]
        [Display(Name = "What is your wish", Prompt = "Swim with whale sharks")]



        //example "I wish to swim with whale sharks"
        public string KidWish { get; set; }


        //details
        //textarea
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Tell me more about your wish")]
        public string Desc { get; set; }
        //the start date shouldn't be showed to the user

        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat]
        public DateTime StartDate { get; set; }

        //TODO: starting from now (date and time ) 

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "To be realised in :")]
        public DateTime ExpirationDate { get; set; }


        //scrolling numbers
        //between 10dt to +oo
        [Required]
        [Range(10.0, Double.MaxValue)]
        [DataType(DataType.Currency)]
        [Display(Name = "Your fundraising goal : ")]
        public double FundToRaise { get; set; }


        //not showed at the begining
        // fundRaised [0-FundToRaise]
        public double FundRaised { get; set; }


        //the wish form woun't be showed until the kid 
        //is already registerd
        [ForeignKey("Kid")]
        public int KidID { get; set; }



        //navigation Fundraiser acts as relationship identifier , 
        //a wish is created by one fundraiser
        public virtual Kid Kid { get; set; }

        //between the wish and the user there is a donation table 
    }

}
