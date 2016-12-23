namespace WhereAreMyMoney_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedOperationsAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Operations", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Operations", "AccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.Operations", "CategoryId");
            CreateIndex("dbo.Operations", "AccountId");
            AddForeignKey("dbo.Operations", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Operations", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            DropColumn("dbo.Operations", "Category_Id");
            DropColumn("dbo.Operations", "Account_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Operations", "Account_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Operations", "Category_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Operations", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Operations", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Operations", new[] { "AccountId" });
            DropIndex("dbo.Operations", new[] { "CategoryId" });
            DropColumn("dbo.Operations", "AccountId");
            DropColumn("dbo.Operations", "CategoryId");
        }
    }
}
