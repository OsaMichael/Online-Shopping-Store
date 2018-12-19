namespace FoodPalace.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductsgg : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "CreatedDate");
            DropColumn("dbo.Products", "CreatedBy");
            DropColumn("dbo.Products", "ModifiedDate");
            DropColumn("dbo.Products", "ModifiedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ModifiedBy", c => c.Long());
            AddColumn("dbo.Products", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.Products", "CreatedBy", c => c.Long(nullable: false));
            AddColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
