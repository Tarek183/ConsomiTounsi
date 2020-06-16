using ConsomiTounsi.data.Configurations;
using ConsomiTounsi.domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ConsomiTounsi.data
{
    public class MyContext : IdentityDbContext<User>
    {
        public new string[] Roles = { "Admin", "DeliveryBoy", "Member" };
        public MyContext() : base("Machaine")
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
        public DbSet<Coupons> Coupons { get; set; }
        public DbSet<Panier> Paniers { get; set; }
        public DbSet<LigneCommand> LigneCommands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rayon> Rayons { get; set; }
        public DbSet<Event> Events { get; set; }
        //les dbsets 
        public DbSet<Wish> Wishes { get; set; }
        public DbSet<FundRaiser> Fundraisers { get; set; }
        public DbSet<Kid> Kids { get; set; }
        public DbSet<Donation> Donations { get; set; }
        //public DbSet<User> users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new CouponConfiguration());
            modelBuilder.Configurations.Add(new LigneCommandConfiguration());
            modelBuilder.Configurations.Add(new PanierConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Conventions.Add(new DateTimeConvention());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public class ContexInit : DropCreateDatabaseIfModelChanges<MyContext>
        {
            protected override void Seed(MyContext context)
            {

            }
        }
    }
}

