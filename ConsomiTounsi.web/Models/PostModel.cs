using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.Models
{
    public class PostModel
    {
        [Key]
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string UrlImage { get; set; }
        public virtual ICollection<Comment> ListComments { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }
        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}