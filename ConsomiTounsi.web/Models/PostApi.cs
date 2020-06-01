using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.Models
{
    public class PostApi
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string UrlImage { get; set; }
        public int Note { get; set; }
        public string UserId { get; set; }
    }
}