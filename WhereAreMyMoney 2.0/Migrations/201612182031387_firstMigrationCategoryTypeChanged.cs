namespace WhereAreMyMoney_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigrationCategoryTypeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Type", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Type", c => c.Boolean(nullable: false));
        }
    }
}
