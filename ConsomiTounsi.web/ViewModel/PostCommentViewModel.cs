using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.ViewModel
{
    public class PostCommentViewModel
    { 
        //public Post Post { get; set; }
        //public Comment Comment { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}