using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    public class Comment
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
        public virtual Post Post { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
