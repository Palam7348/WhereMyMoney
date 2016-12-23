namespace WhereAreMyMoney_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Operations", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Operations", "Name", c => c.String());
        }
    }
}
