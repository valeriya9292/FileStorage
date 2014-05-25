namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrmFiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Path = c.String(),
                        Size = c.Int(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrmUsers", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.OrmUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrmRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.OrmRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrmUsers", "RoleId", "dbo.OrmRoles");
            DropForeignKey("dbo.OrmFiles", "OwnerId", "dbo.OrmUsers");
            DropIndex("dbo.OrmUsers", new[] { "RoleId" });
            DropIndex("dbo.OrmFiles", new[] { "OwnerId" });
            DropTable("dbo.OrmRoles");
            DropTable("dbo.OrmUsers");
            DropTable("dbo.OrmFiles");
        }
    }
}
