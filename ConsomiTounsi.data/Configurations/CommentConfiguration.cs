using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.data.Configurations
{
    class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            HasOptional(Com => Com.Post)
               .WithMany(post => post.ListComments)
               .HasForeignKey(Com => Com.PostId)
               .WillCascadeOnDelete(true);
        }
    }
}
