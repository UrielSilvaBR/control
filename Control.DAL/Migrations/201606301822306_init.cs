namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAdressBook",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        MailContactDescription = c.String(),
                        MailContactName = c.String(),
                        MailContactEmai = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(),
                        RazaoSocial = c.String(),
                        Fantasia = c.String(),
                        InscricaoEstadual = c.Long(),
                        InscricaoMunicipal = c.Long(),
                        CNPJ = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyID)
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
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        IBGECode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(),
                        VendorID = c.Int(),
                        ContatName = c.String(),
                        ContatRoleName = c.String(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        AddressStreet = c.String(),
                        AddressNumber = c.String(),
                        AddressDistrict = c.String(),
                        AddressCity = c.String(),
                        AddressState = c.String(),
                        ZipCode = c.Int(),
                        RegisterDate = c.DateTime(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.Vendor", t => t.VendorID)
                .Index(t => t.CustomerID)
                .Index(t => t.VendorID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        ShortName = c.String(),
                        CustomerType = c.Int(nullable: false),
                        AddressStreet = c.String(),
                        AddressNumber = c.String(),
                        AddressDistrict = c.String(),
                        AddressCityId = c.Int(nullable: false),
                        AddressStateId = c.Int(nullable: false),
                        AddressCountryId = c.String(),
                        AddressComplement = c.String(),
                        ZipCode = c.String(),
                        PhoneCode = c.Int(),
                        Phone = c.Int(),
                        PhoneFax = c.Int(),
                        Email = c.String(),
                        Website = c.String(),
                        Document = c.String(),
                        RegisterDate = c.DateTime(),
                        LastUpdate = c.DateTime(),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        CommercialPolicy = c.Int(),
                        PaymentTermId = c.Int(),
                        ShippingId = c.Int(),
                        VendorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.AddressCityId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentTerm", t => t.PaymentTermId)
                .ForeignKey("dbo.ShippingMode", t => t.ShippingId)
                .ForeignKey("dbo.Vendor", t => t.VendorId)
                .Index(t => t.AddressCityId)
                .Index(t => t.PaymentTermId)
                .Index(t => t.ShippingId)
                .Index(t => t.VendorId);
            
            CreateTable(
                "dbo.PaymentTerm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ShortDescription = c.String(),
                        Days = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Aliquota = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShippingMode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        isWeightCharged = c.Boolean(nullable: false),
                        isVolumeCharged = c.Boolean(nullable: false),
                        WeightPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VolumePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeightUnit = c.String(),
                        VolumeUnit = c.String(),
                        isClientCharged = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vendor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PercentCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VendorsCustomer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        VendorID = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendor", t => t.VendorID, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.VendorID);
            
            CreateTable(
                "dbo.InvoiceItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        SequencialItem = c.Int(nullable: false),
                        QuantityOrder = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityDeliver = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.Int(),
                        Name = c.String(),
                        Brand = c.String(),
                        Model = c.String(),
                        Description = c.String(),
                        UnitMeasure = c.String(),
                        Height = c.String(),
                        Width = c.String(),
                        Lenght = c.String(),
                        Weight = c.Decimal(precision: 18, scale: 2),
                        UnitPrice = c.Decimal(precision: 18, scale: 2),
                        CostPrice = c.Decimal(precision: 18, scale: 2),
                        AliqICMS = c.Decimal(precision: 18, scale: 2),
                        MinimumStockAlert = c.Decimal(precision: 18, scale: 2),
                        ImageURL = c.String(),
                        CombinedProduct = c.Boolean(),
                        NCMCode = c.String(),
                        DescriptionNCC = c.String(),
                        ProductTypeUnitID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeUnit", t => t.ProductTypeUnitID)
                .Index(t => t.ProductTypeUnitID);
            
            CreateTable(
                "dbo.TypeUnit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Sign = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceRps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.Long(nullable: false),
                        InvoiceSerieID = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataEmissao = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        OrderDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.InvoiceSerie", t => t.InvoiceSerieID, cascadeDelete: true)
                .Index(t => t.InvoiceSerieID)
                .Index(t => t.CustomerID);
            
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
            
            CreateTable(
                "dbo.MessageLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(),
                        Description = c.String(),
                        User = c.String(),
                        MessageDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        Comments = c.String(),
                        CFOP = c.Int(),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceNumber = c.Int(),
                        InvoiceStatus = c.Int(),
                        CustomerID = c.Int(nullable: false),
                        OrderTypeID = c.Int(nullable: false),
                        VendorID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        PaymentTermID = c.Int(),
                        ShippingId = c.Int(),
                        DespesaFinanceira = c.Boolean(nullable: false),
                        Validated = c.Boolean(nullable: false),
                        AdminRoleAuthorized = c.Boolean(nullable: false),
                        RegisteredBy = c.String(),
                        CustomerControlCode = c.String(),
                        ProposalMailList = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.PaymentTerm", t => t.PaymentTermID)
                .ForeignKey("dbo.PaymentTerm", t => t.ShippingId)
                .ForeignKey("dbo.OrderType", t => t.OrderTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Vendor", t => t.VendorID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.OrderTypeID)
                .Index(t => t.VendorID)
                .Index(t => t.ContactID)
                .Index(t => t.PaymentTermID)
                .Index(t => t.ShippingId);
            
            CreateTable(
                "dbo.OrderProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        SequencialItem = c.Int(nullable: false),
                        QuantityOrder = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityDeliver = c.Decimal(precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeadlineItem = c.Int(nullable: false),
                        ProductID = c.Int(),
                        TypeUnitID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .ForeignKey("dbo.TypeUnit", t => t.TypeUnitID)
                .Index(t => t.OrderId)
                .Index(t => t.ProductID)
                .Index(t => t.TypeUnitID);
            
            CreateTable(
                "dbo.OrderType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Provider",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        ShortName = c.String(),
                        AddressStreet = c.String(),
                        AddressNumber = c.String(),
                        AddressDistrict = c.String(),
                        AddressCity = c.String(),
                        AddressState = c.String(),
                        ZipCode = c.Int(),
                        PhoneCode = c.Int(),
                        Phone = c.Int(),
                        PhoneFax = c.Int(),
                        Email = c.String(),
                        Website = c.String(),
                        Document = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommercialPolicy = c.Int(),
                        RegisterDate = c.DateTime(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Storage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(),
                        TypeUnitID = c.Int(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .ForeignKey("dbo.Unit", t => t.TypeUnitID)
                .Index(t => t.ProductID)
                .Index(t => t.TypeUnitID);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransType = c.String(),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionID = c.String(),
                        Name = c.String(),
                        Memo = c.String(),
                        IncorrectTransactionID = c.String(),
                        TransactionCorrectionAction = c.String(),
                        ServerTransactionID = c.String(),
                        CheckNum = c.String(),
                        ReferenceNumber = c.String(),
                        Sic = c.String(),
                        PayeeID = c.String(),
                        Currency = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        PasswordOld = c.String(),
                        DateCreated = c.DateTime(),
                        Activated = c.Boolean(),
                        UserRole = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.Storage", "TypeUnitID", "dbo.Unit");
            DropForeignKey("dbo.Storage", "ProductID", "dbo.Product");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PurchaseOrder", "ProviderID", "dbo.Provider");
            DropForeignKey("dbo.PurchaseOrderItem", "PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrderItem", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Order", "VendorID", "dbo.Vendor");
            DropForeignKey("dbo.Order", "OrderTypeID", "dbo.OrderType");
            DropForeignKey("dbo.Order", "ShippingId", "dbo.PaymentTerm");
            DropForeignKey("dbo.Order", "PaymentTermID", "dbo.PaymentTerm");
            DropForeignKey("dbo.OrderProduct", "TypeUnitID", "dbo.TypeUnit");
            DropForeignKey("dbo.OrderProduct", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderProduct", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Order", "ContactID", "dbo.Contact");
            DropForeignKey("dbo.InvoiceTax", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.Invoice", "InvoiceSerieID", "dbo.InvoiceSerie");
            DropForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.Invoice", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.InvoiceItem", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "ProductTypeUnitID", "dbo.TypeUnit");
            DropForeignKey("dbo.Contact", "VendorID", "dbo.Vendor");
            DropForeignKey("dbo.Contact", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.VendorsCustomer", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.VendorsCustomer", "VendorID", "dbo.Vendor");
            DropForeignKey("dbo.Customer", "VendorId", "dbo.Vendor");
            DropForeignKey("dbo.Customer", "ShippingId", "dbo.ShippingMode");
            DropForeignKey("dbo.Customer", "PaymentTermId", "dbo.PaymentTerm");
            DropForeignKey("dbo.Customer", "AddressCityId", "dbo.City");
            DropForeignKey("dbo.City", "StateId", "dbo.State");
            DropForeignKey("dbo.State", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Branch", "CompanyID", "dbo.Company");
            DropIndex("dbo.UserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Storage", new[] { "TypeUnitID" });
            DropIndex("dbo.Storage", new[] { "ProductID" });
            DropIndex("dbo.UserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.PurchaseOrder", new[] { "ProviderID" });
            DropIndex("dbo.PurchaseOrderItem", new[] { "ProductID" });
            DropIndex("dbo.PurchaseOrderItem", new[] { "PurchaseOrderId" });
            DropIndex("dbo.OrderProduct", new[] { "TypeUnitID" });
            DropIndex("dbo.OrderProduct", new[] { "ProductID" });
            DropIndex("dbo.OrderProduct", new[] { "OrderId" });
            DropIndex("dbo.Order", new[] { "ShippingId" });
            DropIndex("dbo.Order", new[] { "PaymentTermID" });
            DropIndex("dbo.Order", new[] { "ContactID" });
            DropIndex("dbo.Order", new[] { "VendorID" });
            DropIndex("dbo.Order", new[] { "OrderTypeID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.InvoiceTax", new[] { "InvoiceId" });
            DropIndex("dbo.Invoice", new[] { "CustomerID" });
            DropIndex("dbo.Invoice", new[] { "InvoiceSerieID" });
            DropIndex("dbo.Product", new[] { "ProductTypeUnitID" });
            DropIndex("dbo.InvoiceItem", new[] { "ProductID" });
            DropIndex("dbo.InvoiceItem", new[] { "InvoiceId" });
            DropIndex("dbo.VendorsCustomer", new[] { "VendorID" });
            DropIndex("dbo.VendorsCustomer", new[] { "CustomerID" });
            DropIndex("dbo.Customer", new[] { "VendorId" });
            DropIndex("dbo.Customer", new[] { "ShippingId" });
            DropIndex("dbo.Customer", new[] { "PaymentTermId" });
            DropIndex("dbo.Customer", new[] { "AddressCityId" });
            DropIndex("dbo.Contact", new[] { "VendorID" });
            DropIndex("dbo.Contact", new[] { "CustomerID" });
            DropIndex("dbo.State", new[] { "CountryId" });
            DropIndex("dbo.City", new[] { "StateId" });
            DropIndex("dbo.Branch", new[] { "CompanyID" });
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Transaction");
            DropTable("dbo.Unit");
            DropTable("dbo.Storage");
            DropTable("dbo.Stock");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.PurchaseOrderItem");
            DropTable("dbo.Provider");
            DropTable("dbo.OrderType");
            DropTable("dbo.OrderProduct");
            DropTable("dbo.Order");
            DropTable("dbo.MessageLog");
            DropTable("dbo.InvoiceTax");
            DropTable("dbo.InvoiceSerie");
            DropTable("dbo.Invoice");
            DropTable("dbo.InvoiceRps");
            DropTable("dbo.TypeUnit");
            DropTable("dbo.Product");
            DropTable("dbo.InvoiceItem");
            DropTable("dbo.VendorsCustomer");
            DropTable("dbo.Vendor");
            DropTable("dbo.ShippingMode");
            DropTable("dbo.PaymentTerm");
            DropTable("dbo.Customer");
            DropTable("dbo.Contact");
            DropTable("dbo.Country");
            DropTable("dbo.State");
            DropTable("dbo.City");
            DropTable("dbo.Company");
            DropTable("dbo.Branch");
            DropTable("dbo.UserAdressBook");
        }
    }
}
