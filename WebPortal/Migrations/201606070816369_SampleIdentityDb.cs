namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleIdentityDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WebPortalRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.WebPortalUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.WebPortalRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.WebPortalUsers", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.WebPortalUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebPortalUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebPortalUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.WebPortalUserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.WebPortalUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebPortalUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Id", "dbo.WebPortalUsers");
            DropForeignKey("dbo.WebPortalUserRoles", "IdentityUser_Id", "dbo.WebPortalUsers");
            DropForeignKey("dbo.WebPortalUserLogin", "IdentityUser_Id", "dbo.WebPortalUsers");
            DropForeignKey("dbo.WebPortalUserClaims", "IdentityUser_Id", "dbo.WebPortalUsers");
            DropForeignKey("dbo.WebPortalUserRoles", "RoleId", "dbo.WebPortalRoles");
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropIndex("dbo.WebPortalUserLogin", new[] { "IdentityUser_Id" });
            DropIndex("dbo.WebPortalUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.WebPortalUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.WebPortalUserRoles", new[] { "RoleId" });
            DropIndex("dbo.WebPortalRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.WebPortalUserLogin");
            DropTable("dbo.WebPortalUserClaims");
            DropTable("dbo.WebPortalUsers");
            DropTable("dbo.WebPortalUserRoles");
            DropTable("dbo.WebPortalRoles");
        }
    }
}
