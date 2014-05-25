namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTypeOfSizeField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrmFiles", "Size", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrmFiles", "Size", c => c.Int(nullable: false));
        }
    }
}
