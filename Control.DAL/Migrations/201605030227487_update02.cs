namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        RazaoSocial = c.String(),
                        Fantasia = c.String(),
                        InscricaoEstadual = c.Long(nullable: false),
                        InscricaoMunicipal = c.Long(nullable: false),
                        CNPJ = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RazaoSocial = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceSerie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceTax",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        ValorIss = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorPis = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorCofins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorInss = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorIr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorCsll = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Aliquota = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
            AddColumn("dbo.InvoiceItem", "InvoiceId", c => c.Int(nullable: false));
            AddColumn("dbo.InvoiceItem", "SequencialItem", c => c.Int(nullable: false));
            AddColumn("dbo.InvoiceItem", "QuantityOrder", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceItem", "QuantityDeliver", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceItem", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceItem", "ItemDiscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceItem", "Comments", c => c.String());
            AddColumn("dbo.InvoiceItem", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceItem", "ProductID", c => c.Int(nullable: false));
            AddColumn("dbo.Invoice", "Numero", c => c.Long(nullable: false));
            AddColumn("dbo.Invoice", "InvoiceSerieID", c => c.Int(nullable: false));
            AddColumn("dbo.Invoice", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Invoice", "DataEmissao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Invoice", "Status", c => c.String());
            AddColumn("dbo.Invoice", "InsertDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Invoice", "OrderDate", c => c.DateTime());
            AddColumn("dbo.Invoice", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Invoice", "CustomerID", c => c.Int());
            AlterColumn("dbo.Contact", "CustomerID", c => c.Int());
            AlterColumn("dbo.Contact", "ZipCode", c => c.Int());
            AlterColumn("dbo.Contact", "RegisterDate", c => c.DateTime());
            AlterColumn("dbo.Contact", "LastUpdate", c => c.DateTime());
            AlterColumn("dbo.Customer", "ZipCode", c => c.Int());
            AlterColumn("dbo.Customer", "PhoneCode", c => c.Int());
            AlterColumn("dbo.Customer", "Phone", c => c.Int());
            AlterColumn("dbo.Customer", "PhoneFax", c => c.Int());
            AlterColumn("dbo.Customer", "Document", c => c.Long(nullable: false));
            AlterColumn("dbo.Customer", "CommercialPolicy", c => c.Int());
            AlterColumn("dbo.Customer", "RegisterDate", c => c.DateTime());
            AlterColumn("dbo.Customer", "LastUpdate", c => c.DateTime());
            AlterColumn("dbo.MessageLog", "Code", c => c.Int());
            AlterColumn("dbo.Order", "CFOP", c => c.Int());
            AlterColumn("dbo.Order", "Discount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Order", "TotalValue", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Order", "InvoiceNumber", c => c.Int());
            AlterColumn("dbo.Order", "InvoiceStatus", c => c.Int());
            AlterColumn("dbo.OrderProduct", "QuantityOrder", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "QuantityDeliver", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "UnitPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "ItemDiscount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Provider", "ZipCode", c => c.Int());
            AlterColumn("dbo.Provider", "PhoneCode", c => c.Int());
            AlterColumn("dbo.Provider", "Phone", c => c.Int());
            AlterColumn("dbo.Provider", "PhoneFax", c => c.Int());
            AlterColumn("dbo.Provider", "CommercialPolicy", c => c.Int());
            AlterColumn("dbo.Provider", "RegisterDate", c => c.DateTime());
            AlterColumn("dbo.Provider", "LastUpdate", c => c.DateTime());
            CreateIndex("dbo.InvoiceItem", "InvoiceId");
            CreateIndex("dbo.InvoiceItem", "ProductID");
            CreateIndex("dbo.Invoice", "InvoiceSerieID");
            CreateIndex("dbo.Invoice", "CustomerID");
            AddForeignKey("dbo.InvoiceItem", "ProductID", "dbo.Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Invoice", "CustomerID", "dbo.Customer", "Id");
            AddForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Invoice", "InvoiceSerieID", "dbo.InvoiceSerie", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceTax", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.Invoice", "InvoiceSerieID", "dbo.InvoiceSerie");
            DropForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.Invoice", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.InvoiceItem", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Branch", "CompanyID", "dbo.Company");
            DropIndex("dbo.InvoiceTax", new[] { "InvoiceId" });
            DropIndex("dbo.Invoice", new[] { "CustomerID" });
            DropIndex("dbo.Invoice", new[] { "InvoiceSerieID" });
            DropIndex("dbo.InvoiceItem", new[] { "ProductID" });
            DropIndex("dbo.InvoiceItem", new[] { "InvoiceId" });
            DropIndex("dbo.Branch", new[] { "CompanyID" });
            AlterColumn("dbo.Provider", "LastUpdate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Provider", "RegisterDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Provider", "CommercialPolicy", c => c.Int(nullable: false));
            AlterColumn("dbo.Provider", "PhoneFax", c => c.Int(nullable: false));
            AlterColumn("dbo.Provider", "Phone", c => c.Int(nullable: false));
            AlterColumn("dbo.Provider", "PhoneCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Provider", "ZipCode", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderProduct", "ItemDiscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "QuantityDeliver", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderProduct", "QuantityOrder", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Order", "InvoiceStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "InvoiceNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "TotalValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Order", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Order", "CFOP", c => c.Int(nullable: false));
            AlterColumn("dbo.MessageLog", "Code", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer", "LastUpdate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customer", "RegisterDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customer", "CommercialPolicy", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer", "Document", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer", "PhoneFax", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer", "Phone", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer", "PhoneCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer", "ZipCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Contact", "LastUpdate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Contact", "RegisterDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Contact", "ZipCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Contact", "CustomerID", c => c.Int(nullable: false));
            DropColumn("dbo.Invoice", "CustomerID");
            DropColumn("dbo.Invoice", "UpdateDate");
            DropColumn("dbo.Invoice", "OrderDate");
            DropColumn("dbo.Invoice", "InsertDate");
            DropColumn("dbo.Invoice", "Status");
            DropColumn("dbo.Invoice", "DataEmissao");
            DropColumn("dbo.Invoice", "Valor");
            DropColumn("dbo.Invoice", "InvoiceSerieID");
            DropColumn("dbo.Invoice", "Numero");
            DropColumn("dbo.InvoiceItem", "ProductID");
            DropColumn("dbo.InvoiceItem", "TotalPrice");
            DropColumn("dbo.InvoiceItem", "Comments");
            DropColumn("dbo.InvoiceItem", "ItemDiscount");
            DropColumn("dbo.InvoiceItem", "UnitPrice");
            DropColumn("dbo.InvoiceItem", "QuantityDeliver");
            DropColumn("dbo.InvoiceItem", "QuantityOrder");
            DropColumn("dbo.InvoiceItem", "SequencialItem");
            DropColumn("dbo.InvoiceItem", "InvoiceId");
            DropTable("dbo.InvoiceTax");
            DropTable("dbo.InvoiceSerie");
            DropTable("dbo.Company");
            DropTable("dbo.Branch");
        }
    }
}
