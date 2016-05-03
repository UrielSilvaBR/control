namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class publish02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "ProductCode", c => c.Int());
            AlterColumn("dbo.Product", "Weight", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Product", "UnitPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Product", "CostPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Product", "AliqICMS", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Product", "MinimumStockAlert", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Product", "CombinedProduct", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "CombinedProduct", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Product", "MinimumStockAlert", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Product", "AliqICMS", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Product", "CostPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Product", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Product", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Product", "ProductCode", c => c.Int(nullable: false));
        }
    }
}
