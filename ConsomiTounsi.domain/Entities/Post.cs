using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string UrlImage { get; set; }
        public virtual ICollection<Comment> ListComments { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId ")]
        public virtual User User { get; set; }
    }
}
