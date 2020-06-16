namespace ConsomiTounsi.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        ContentComment = c.String(),
                        CommentDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LikeComment = c.Int(nullable: false),
                        DislikeComment = c.Int(nullable: false),
                        PostId = c.Int(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.PostId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        PublishDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UrlImage = c.String(),
                        Like = c.Int(nullable: false),
                        DisLike = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Phone = c.String(),
                        image = c.String(),
                        Role = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        IdContact = c.Int(nullable: false, identity: true),
                        ContactContent = c.String(),
                        UserName = c.String(),
                        UserEmail = c.String(),
                        UserPhone = c.String(),
                        ContactDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.IdContact)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.coupon",
                c => new
                    {
                        idcoupon = c.Int(nullable: false, identity: true),
                        duree = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        percontage = c.Single(nullable: false),
                        date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        etat = c.Int(nullable: false),
                        panierId = c.Int(),
                    })
                .PrimaryKey(t => t.idcoupon)
                .ForeignKey("dbo.panier", t => t.panierId)
                .Index(t => t.panierId);
            
            CreateTable(
                "dbo.panier",
                c => new
                    {
                        idPanier = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ClientId = c.Int(nullable: false),
                        Validation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idPanier);
            
            CreateTable(
                "dbo.LigneCommand",
                c => new
                    {
                        idLigneCommand = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        quantite = c.Int(nullable: false),
                        PanierId = c.Int(nullable: false),
                        ProduitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idLigneCommand)
                .ForeignKey("dbo.panier", t => t.PanierId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProduitId, cascadeDelete: true)
                .Index(t => t.PanierId)
                .Index(t => t.ProduitId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        IdProduct = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        imageP = c.String(),
                        description = c.String(),
                        CategorieP = c.Int(nullable: false),
                        barcode = c.String(),
                        marque = c.String(),
                        price = c.Double(nullable: false),
                        IdRayon = c.Int(),
                        dateC = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        dateF = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        qte = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProduct)
                .ForeignKey("dbo.Rayon", t => t.IdRayon, cascadeDelete: true)
                .Index(t => t.IdRayon);
            
            CreateTable(
                "dbo.Rayon",
                c => new
                    {
                        IdRayon = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        typeR = c.String(),
                        imageR = c.String(),
                        flag = c.String(),
                    })
                .PrimaryKey(t => t.IdRayon);
            
            CreateTable(
                "dbo.Donation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(maxLength: 128),
                        WishId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Wish", t => t.WishId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WishId);
            
            CreateTable(
                "dbo.Wish",
                c => new
                    {
                        WishID = c.Int(nullable: false, identity: true),
                        KidWish = c.String(nullable: false, maxLength: 30),
                        Desc = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ExpirationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FundToRaise = c.Double(nullable: false),
                        FundRaised = c.Double(nullable: false),
                        KidID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WishID)
                .ForeignKey("dbo.Kid", t => t.KidID, cascadeDelete: true)
                .Index(t => t.KidID);
            
            CreateTable(
                "dbo.Kid",
                c => new
                    {
                        KidID = c.Int(nullable: false, identity: true),
                        KidFistName = c.String(nullable: false),
                        KidSecondName = c.String(nullable: false),
                        KidBirthdate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        KidDiagnosis = c.String(nullable: false),
                        MedicalProfessionalEmail = c.String(nullable: false),
                        ImagePath = c.String(),
                        Age = c.Int(nullable: false),
                        RelationshipToChild = c.Int(nullable: false),
                        RelationSpecification = c.String(),
                        OrganizationName = c.String(),
                        FundRaiserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KidID)
                .ForeignKey("dbo.FundRaiser", t => t.FundRaiserID, cascadeDelete: true)
                .Index(t => t.FundRaiserID);
            
            CreateTable(
                "dbo.FundRaiser",
                c => new
                    {
                        FundRaiserID = c.Int(nullable: false, identity: true),
                        CIN = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        BirthDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PhoneNumber = c.String(nullable: false, maxLength: 8),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FundRaiserID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        employeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        email = c.String(),
                        phoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.employeId);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        IdEvent = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        imageE = c.String(),
                        description = c.String(),
                        dateD = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        dateF = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        hours = c.Int(nullable: false),
                        date_publication = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        deadline = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        nbApply = c.Int(nullable: false),
                        nbVue = c.Int(nullable: false),
                        IdProduct = c.Int(nullable: false),
                        nbRate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdEvent);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Donation", "WishId", "dbo.Wish");
            DropForeignKey("dbo.Wish", "KidID", "dbo.Kid");
            DropForeignKey("dbo.Kid", "FundRaiserID", "dbo.FundRaiser");
            DropForeignKey("dbo.Donation", "UserId", "dbo.User");
            DropForeignKey("dbo.coupon", "panierId", "dbo.panier");
            DropForeignKey("dbo.LigneCommand", "ProduitId", "dbo.Product");
            DropForeignKey("dbo.Product", "IdRayon", "dbo.Rayon");
            DropForeignKey("dbo.LigneCommand", "PanierId", "dbo.panier");
            DropForeignKey("dbo.Comment", "UserId", "dbo.User");
            DropForeignKey("dbo.Comment", "PostId", "dbo.Post");
            DropForeignKey("dbo.IdentityUserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.Post", "UserId", "dbo.User");
            DropForeignKey("dbo.IdentityUserLogin", "User_Id", "dbo.User");
            DropForeignKey("dbo.Contact", "UserId", "dbo.User");
            DropForeignKey("dbo.IdentityUserClaim", "UserId", "dbo.User");
            DropIndex("dbo.Kid", new[] { "FundRaiserID" });
            DropIndex("dbo.Wish", new[] { "KidID" });
            DropIndex("dbo.Donation", new[] { "WishId" });
            DropIndex("dbo.Donation", new[] { "UserId" });
            DropIndex("dbo.Product", new[] { "IdRayon" });
            DropIndex("dbo.LigneCommand", new[] { "ProduitId" });
            DropIndex("dbo.LigneCommand", new[] { "PanierId" });
            DropIndex("dbo.coupon", new[] { "panierId" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "UserId" });
            DropIndex("dbo.IdentityUserLogin", new[] { "User_Id" });
            DropIndex("dbo.Contact", new[] { "UserId" });
            DropIndex("dbo.IdentityUserClaim", new[] { "UserId" });
            DropIndex("dbo.Post", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "PostId" });
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Event");
            DropTable("dbo.Employee");
            DropTable("dbo.FundRaiser");
            DropTable("dbo.Kid");
            DropTable("dbo.Wish");
            DropTable("dbo.Donation");
            DropTable("dbo.Rayon");
            DropTable("dbo.Product");
            DropTable("dbo.LigneCommand");
            DropTable("dbo.panier");
            DropTable("dbo.coupon");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.Contact");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.User");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
        }
    }
}
