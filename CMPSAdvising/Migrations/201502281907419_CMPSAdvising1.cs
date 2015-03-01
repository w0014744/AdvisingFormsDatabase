namespace CMPSAdvising.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CMPSAdvising1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Selected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Selected", c => c.Boolean());
        }
    }
}
