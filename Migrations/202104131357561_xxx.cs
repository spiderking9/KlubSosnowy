namespace KlubSosnowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxx : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Potrawies",
                c => new
                    {
                        IdPotrawy = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IdPotrawy);
            
            CreateTable(
                "dbo.Potrawy_Skladniki",
                c => new
                    {
                        IdPotrawy_Skladnika = c.Int(nullable: false, identity: true),
                        IdPotrawy = c.Int(nullable: false),
                        IdSkladnika = c.Int(nullable: false),
                        Ilosc = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdPotrawy_Skladnika)
                .ForeignKey("dbo.Potrawies", t => t.IdPotrawy, cascadeDelete: true)
                .ForeignKey("dbo.Skladnikis", t => t.IdSkladnika, cascadeDelete: true)
                .Index(t => t.IdPotrawy)
                .Index(t => t.IdSkladnika);
            
            CreateTable(
                "dbo.Skladnikis",
                c => new
                    {
                        IdSkladnika = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        JednostkaMiary = c.String(),
                    })
                .PrimaryKey(t => t.IdSkladnika);
            
            CreateTable(
                "dbo.PozycjeZamowienias",
                c => new
                    {
                        IdPozycjeZamowienia = c.Int(nullable: false, identity: true),
                        IdZamowienia = c.Int(nullable: false),
                        IdPotrawy = c.Int(nullable: false),
                        Ilosc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPozycjeZamowienia)
                .ForeignKey("dbo.Potrawies", t => t.IdPotrawy, cascadeDelete: true)
                .ForeignKey("dbo.Zamowienias", t => t.IdZamowienia, cascadeDelete: true)
                .Index(t => t.IdZamowienia)
                .Index(t => t.IdPotrawy);
            
            CreateTable(
                "dbo.Zamowienias",
                c => new
                    {
                        IdZamowienia = c.Int(nullable: false, identity: true),
                        IdKlienta = c.Int(nullable: false),
                        DataDodania = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdZamowienia);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PozycjeZamowienias", "IdZamowienia", "dbo.Zamowienias");
            DropForeignKey("dbo.PozycjeZamowienias", "IdPotrawy", "dbo.Potrawies");
            DropForeignKey("dbo.Potrawy_Skladniki", "IdSkladnika", "dbo.Skladnikis");
            DropForeignKey("dbo.Potrawy_Skladniki", "IdPotrawy", "dbo.Potrawies");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PozycjeZamowienias", new[] { "IdPotrawy" });
            DropIndex("dbo.PozycjeZamowienias", new[] { "IdZamowienia" });
            DropIndex("dbo.Potrawy_Skladniki", new[] { "IdSkladnika" });
            DropIndex("dbo.Potrawy_Skladniki", new[] { "IdPotrawy" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Zamowienias");
            DropTable("dbo.PozycjeZamowienias");
            DropTable("dbo.Skladnikis");
            DropTable("dbo.Potrawy_Skladniki");
            DropTable("dbo.Potrawies");
        }
    }
}
