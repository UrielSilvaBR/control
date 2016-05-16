namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pedido_Inclusao : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "ContactID", "dbo.Contact");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Order", "OrderTypeID", "dbo.OrderType");
            DropForeignKey("dbo.Order", "VendorID", "dbo.Vendor");
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.Order", new[] { "OrderTypeID" });
            DropIndex("dbo.Order", new[] { "VendorID" });
            DropIndex("dbo.Order", new[] { "ContactID" });
            AlterColumn("dbo.Order", "UpdateDate", c => c.DateTime());
            AlterColumn("dbo.Order", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Order", "TotalValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Order", "CustomerID", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "OrderTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "VendorID", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "ContactID", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderProduct", "QuantityOrder", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "ItemDiscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Order", "CustomerID");
            CreateIndex("dbo.Order", "OrderTypeID");
            CreateIndex("dbo.Order", "VendorID");
            CreateIndex("dbo.Order", "ContactID");
            CreateIndex("dbo.OrderProduct", "OrderId");
            AddForeignKey("dbo.OrderProduct", "OrderId", "dbo.Order", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Order", "ContactID", "dbo.Contact", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Order", "CustomerID", "dbo.Customer", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Order", "OrderTypeID", "dbo.OrderType", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Order", "VendorID", "dbo.Vendor", "Id", cascadeDelete: true);
            DropColumn("dbo.OrderProduct", "ProductName");
            DropColumn("dbo.OrderProduct", "ProductModel");
            DropColumn("dbo.OrderProduct", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderProduct", "Description", c => c.String());
            AddColumn("dbo.OrderProduct", "ProductModel", c => c.String());
            AddColumn("dbo.OrderProduct", "ProductName", c => c.String());
            DropForeignKey("dbo.Order", "VendorID", "dbo.Vendor");
            DropForeignKey("dbo.Order", "OrderTypeID", "dbo.OrderType");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Order", "ContactID", "dbo.Contact");
            DropForeignKey("dbo.OrderProduct", "OrderId", "dbo.Order");
            DropIndex("dbo.OrderProduct", new[] { "OrderId" });
            DropIndex("dbo.Order", new[] { "ContactID" });
            DropIndex("dbo.Order", new[] { "VendorID" });
            DropIndex("dbo.Order", new[] { "OrderTypeID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            AlterColumn("dbo.OrderProduct", "TotalPrice", c => c.String());
            AlterColumn("dbo.OrderProduct", "ItemDiscount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "UnitPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "QuantityOrder", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Order", "ContactID", c => c.Int());
            AlterColumn("dbo.Order", "VendorID", c => c.Int());
            AlterColumn("dbo.Order", "OrderTypeID", c => c.Int());
            AlterColumn("dbo.Order", "CustomerID", c => c.Int());
            AlterColumn("dbo.Order", "TotalValue", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Order", "Discount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Order", "UpdateDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Order", "ContactID");
            CreateIndex("dbo.Order", "VendorID");
            CreateIndex("dbo.Order", "OrderTypeID");
            CreateIndex("dbo.Order", "CustomerID");
            AddForeignKey("dbo.Order", "VendorID", "dbo.Vendor", "Id");
            AddForeignKey("dbo.Order", "OrderTypeID", "dbo.OrderType", "Id");
            AddForeignKey("dbo.Order", "CustomerID", "dbo.Customer", "Id");
            AddForeignKey("dbo.Order", "ContactID", "dbo.Contact", "Id");
        }
    }
}
