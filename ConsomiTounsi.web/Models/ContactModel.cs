using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.Models
{
    public class ContactModel
    {
        [Key]
        public int IdContact { get; set; }
        public string ContactContent { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public DateTime ContactDate { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId ")]
        public virtual User User { get; set; }
    }
}