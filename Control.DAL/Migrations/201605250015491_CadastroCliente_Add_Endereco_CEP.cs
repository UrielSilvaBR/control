namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CadastroCliente_Add_Endereco_CEP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateId = c.Int(nullable: false),
                        Name = c.String(),
                        IBGECode = c.Int(nullable: false),
                        CEPInicial = c.Long(nullable: false),
                        CEPFinal = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.State", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(),
                        UF = c.String(),
                        IBGECode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.PurchaseOrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
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
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
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
            
            AddColumn("dbo.Customer", "CustomerType", c => c.Int(nullable: false));
            AddColumn("dbo.Customer", "AddressCityId", c => c.Int(nullable: false));
            AddColumn("dbo.Customer", "AddressStateId", c => c.Int(nullable: false));
            AddColumn("dbo.Customer", "AddressCountryId", c => c.String());
            AddColumn("dbo.Customer", "AddressComplement", c => c.String());
            AddColumn("dbo.Country", "Code", c => c.String());
            AddColumn("dbo.Country", "IBGECode", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer", "ZipCode", c => c.String());
            AlterColumn("dbo.Customer", "Document", c => c.String());
            DropColumn("dbo.Customer", "AddressCity");
            DropColumn("dbo.Customer", "AddressState");
            DropColumn("dbo.Country", "CountryCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Country", "CountryCode", c => c.String());
            AddColumn("dbo.Customer", "AddressState", c => c.String());
            AddColumn("dbo.Customer", "AddressCity", c => c.String());
            DropForeignKey("dbo.PurchaseOrder", "ProviderID", "dbo.Provider");
            DropForeignKey("dbo.PurchaseOrderItem", "PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrderItem", "ProductID", "dbo.Product");
            DropForeignKey("dbo.City", "StateId", "dbo.State");
            DropForeignKey("dbo.State", "CountryId", "dbo.Country");
            DropIndex("dbo.PurchaseOrder", new[] { "ProviderID" });
            DropIndex("dbo.PurchaseOrderItem", new[] { "ProductID" });
            DropIndex("dbo.PurchaseOrderItem", new[] { "PurchaseOrderId" });
            DropIndex("dbo.State", new[] { "CountryId" });
            DropIndex("dbo.City", new[] { "StateId" });
            AlterColumn("dbo.Customer", "Document", c => c.Long());
            AlterColumn("dbo.Customer", "ZipCode", c => c.Int());
            DropColumn("dbo.Country", "IBGECode");
            DropColumn("dbo.Country", "Code");
            DropColumn("dbo.Customer", "AddressComplement");
            DropColumn("dbo.Customer", "AddressCountryId");
            DropColumn("dbo.Customer", "AddressStateId");
            DropColumn("dbo.Customer", "AddressCityId");
            DropColumn("dbo.Customer", "CustomerType");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.PurchaseOrderItem");
            DropTable("dbo.State");
            DropTable("dbo.City");
        }
    }
}
