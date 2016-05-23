namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21062016_puchaseOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseOrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PuchaseOrderID = c.Int(nullable: false),
                        SequencialItem = c.Int(nullable: false),
                        QuantityOrder = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityDeliver = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProviderID = c.Int(),
                        Status = c.String(),
                        InvoiceNumber = c.Int(),
                        InsertDate = c.DateTime(nullable: false),
                        ValidateDate = c.DateTime(nullable: false),
                        TotalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provider", t => t.ProviderID)
                .Index(t => t.ProviderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrder", "ProviderID", "dbo.Provider");
            DropForeignKey("dbo.PurchaseOrderItem", "ProductID", "dbo.Product");
            DropIndex("dbo.PurchaseOrder", new[] { "ProviderID" });
            DropIndex("dbo.PurchaseOrderItem", new[] { "ProductID" });
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.PurchaseOrderItem");
        }
    }
}
