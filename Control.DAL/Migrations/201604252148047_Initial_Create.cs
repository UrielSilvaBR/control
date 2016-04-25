namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
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
                        ZipCode = c.Int(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
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
                        ZipCode = c.Int(nullable: false),
                        PhoneCode = c.Int(nullable: false),
                        Phone = c.Int(nullable: false),
                        PhoneFax = c.Int(nullable: false),
                        Email = c.String(),
                        Website = c.String(),
                        Document = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommercialPolicy = c.Int(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceItem",
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
                        UpdateDate = c.DateTime(nullable: false),
                        Comments = c.String(),
                        CFOP = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceNumber = c.Int(nullable: false),
                        InvoiceStatus = c.Int(nullable: false),
                        CustomerID = c.Int(),
                        OrderTypeID = c.Int(),
                        VendorID = c.Int(),
                        ContactID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.ContactID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.OrderType", t => t.OrderTypeID)
                .ForeignKey("dbo.Vendor", t => t.VendorID)
                .Index(t => t.CustomerID)
                .Index(t => t.OrderTypeID)
                .Index(t => t.VendorID)
                .Index(t => t.ContactID);
            
            CreateTable(
                "dbo.OrderType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
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
                "dbo.OrderProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        SequencialItem = c.Int(nullable: false),
                        ProductName = c.String(),
                        ProductModel = c.String(),
                        Description = c.String(),
                        QuantityOrder = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityDeliver = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                        TotalPrice = c.String(),
                        ProductID = c.Int(),
                        TypeUnitID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .ForeignKey("dbo.TypeUnit", t => t.TypeUnitID)
                .Index(t => t.ProductID)
                .Index(t => t.TypeUnitID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.Int(nullable: false),
                        Name = c.String(),
                        Brand = c.String(),
                        Model = c.String(),
                        Description = c.String(),
                        UnitMeasure = c.String(),
                        Height = c.String(),
                        Width = c.String(),
                        Lenght = c.String(),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AliqICMS = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinimumStockAlert = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageURL = c.String(),
                        CombinedProduct = c.Boolean(nullable: false),
                        TypeUnitID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeUnit", t => t.TypeUnitID)
                .Index(t => t.TypeUnitID);
            
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
                        ZipCode = c.Int(nullable: false),
                        PhoneCode = c.Int(nullable: false),
                        Phone = c.Int(nullable: false),
                        PhoneFax = c.Int(nullable: false),
                        Email = c.String(),
                        Website = c.String(),
                        Document = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommercialPolicy = c.Int(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.OrderProduct", "TypeUnitID", "dbo.TypeUnit");
            DropForeignKey("dbo.OrderProduct", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "TypeUnitID", "dbo.TypeUnit");
            DropForeignKey("dbo.Order", "VendorID", "dbo.Vendor");
            DropForeignKey("dbo.Order", "OrderTypeID", "dbo.OrderType");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Order", "ContactID", "dbo.Contact");
            DropIndex("dbo.UserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Storage", new[] { "TypeUnitID" });
            DropIndex("dbo.Storage", new[] { "ProductID" });
            DropIndex("dbo.UserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Product", new[] { "TypeUnitID" });
            DropIndex("dbo.OrderProduct", new[] { "TypeUnitID" });
            DropIndex("dbo.OrderProduct", new[] { "ProductID" });
            DropIndex("dbo.Order", new[] { "ContactID" });
            DropIndex("dbo.Order", new[] { "VendorID" });
            DropIndex("dbo.Order", new[] { "OrderTypeID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Transaction");
            DropTable("dbo.Unit");
            DropTable("dbo.Storage");
            DropTable("dbo.Stock");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Provider");
            DropTable("dbo.TypeUnit");
            DropTable("dbo.Product");
            DropTable("dbo.OrderProduct");
            DropTable("dbo.Vendor");
            DropTable("dbo.OrderType");
            DropTable("dbo.Order");
            DropTable("dbo.Invoice");
            DropTable("dbo.InvoiceItem");
            DropTable("dbo.Customer");
            DropTable("dbo.Country");
            DropTable("dbo.Contact");
        }
    }
}
