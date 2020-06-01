using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }
        public string ContentComment { get; set; }
        [DataType(DataType.Date)]
        public DateTime CommentDate { get; set; }
        public int LikeComment { get; set; }
        public int DislikeComment { get; set; }
        public int? PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}