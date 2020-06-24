namespace BookStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "CreatedDate", c => c.Int(nullable: false));
        }
    }
}
