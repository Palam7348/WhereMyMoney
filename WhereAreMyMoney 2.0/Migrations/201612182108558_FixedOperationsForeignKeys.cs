namespace WhereAreMyMoney_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedOperationsForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Operations", "Account_Id1", "dbo.Accounts");
            DropForeignKey("dbo.Operations", "Category_Id1", "dbo.Categories");
            DropIndex("dbo.Operations", new[] { "Account_Id1" });
            DropIndex("dbo.Operations", new[] { "Category_Id1" });
            AddColumn("dbo.Accounts", "UserId", c => c.String());
            DropColumn("dbo.Accounts", "User_Id");
            DropColumn("dbo.Operations", "Account_Id1");
            DropColumn("dbo.Operations", "Category_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Operations", "Category_Id1", c => c.Int());
            AddColumn("dbo.Operations", "Account_Id1", c => c.Int());
            AddColumn("dbo.Accounts", "User_Id", c => c.String());
            DropColumn("dbo.Accounts", "UserId");
            CreateIndex("dbo.Operations", "Category_Id1");
            CreateIndex("dbo.Operations", "Account_Id1");
            AddForeignKey("dbo.Operations", "Category_Id1", "dbo.Categories", "Id");
            AddForeignKey("dbo.Operations", "Account_Id1", "dbo.Accounts", "Id");
        }
    }
}
