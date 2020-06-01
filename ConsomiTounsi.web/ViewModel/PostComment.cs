using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.ViewModel
{
    public class PostComment
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string UrlImage { get; set; }
        public virtual ICollection<Comment> ListComments { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int CommentId { get; set; }
        public string ContentComment { get; set; }
        public DateTime CommentDate { get; set; }
        public int LikeComment { get; set; }
        public int DislikeComment { get; set; }
    }
}