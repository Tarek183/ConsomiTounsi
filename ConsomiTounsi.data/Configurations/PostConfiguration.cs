using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.data.Configurations
{
    class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            //HasMany(post => post.ListComments).WithRequired(com => com.Post).HasForeignKey(post => post.CommentId);
        }

    }
}
