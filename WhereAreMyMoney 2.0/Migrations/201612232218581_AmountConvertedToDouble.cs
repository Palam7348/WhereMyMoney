namespace WhereAreMyMoney_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmountConvertedToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Amount", c => c.Double(nullable: false));
            AlterColumn("dbo.Operations", "Amount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Operations", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.Accounts", "Amount", c => c.Int(nullable: false));
        }
    }
}
