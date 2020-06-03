using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.data.Configurations
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(u => u.Posts).WithRequired(k => k.User).HasForeignKey(p => p.UserId).WillCascadeOnDelete(true);
            HasMany(u => u.Contacts).WithRequired(k => k.User).HasForeignKey(p => p.UserId).WillCascadeOnDelete(true);

        }
    }
}
