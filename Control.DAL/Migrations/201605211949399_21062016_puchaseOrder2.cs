namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21062016_puchaseOrder2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderItem", "PurchaseOrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchaseOrderItem", "PurchaseOrderId");
            AddForeignKey("dbo.PurchaseOrderItem", "PurchaseOrderId", "dbo.PurchaseOrder", "Id", cascadeDelete: true);
            DropColumn("dbo.PurchaseOrderItem", "PuchaseOrderID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderItem", "PuchaseOrderID", c => c.Int(nullable: false));
            DropForeignKey("dbo.PurchaseOrderItem", "PurchaseOrderId", "dbo.PurchaseOrder");
            DropIndex("dbo.PurchaseOrderItem", new[] { "PurchaseOrderId" });
            DropColumn("dbo.PurchaseOrderItem", "PurchaseOrderId");
        }
    }
}
