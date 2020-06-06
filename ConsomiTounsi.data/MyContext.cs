using ConsomiTounsi.data.Configurations;
using ConsomiTounsi.domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.data
{
    public class MyContext : IdentityDbContext<User>
    {
        public new string[] Roles = { "Admin", "DeliveryBoy", "Member" };
        public MyContext() : base("name=Machaine")
        {

        }
        public static MyContext Create()
        {
            return new MyContext();
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
        }
    }
}

