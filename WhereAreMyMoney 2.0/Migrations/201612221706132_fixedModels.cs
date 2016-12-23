namespace WhereAreMyMoney_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Accounts", "Name", c => c.String());
        }
    }
}
