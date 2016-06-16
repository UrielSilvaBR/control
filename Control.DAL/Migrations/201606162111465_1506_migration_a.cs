namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1506_migration_a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VendorsCustomer", "Id", "dbo.Customer");
            DropColumn("dbo.VendorsCustomer", "CustomerID");
            RenameColumn(table: "dbo.VendorsCustomer", name: "Id", newName: "CustomerID");
            RenameIndex(table: "dbo.VendorsCustomer", name: "IX_Id", newName: "IX_CustomerID");
            DropPrimaryKey("dbo.VendorsCustomer");
            AddColumn("dbo.VendorsCustomer", "InsertDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.VendorsCustomer", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.VendorsCustomer", "Id");
            AddForeignKey("dbo.VendorsCustomer", "CustomerID", "dbo.Customer", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VendorsCustomer", "CustomerID", "dbo.Customer");
            DropPrimaryKey("dbo.VendorsCustomer");
            AlterColumn("dbo.VendorsCustomer", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.VendorsCustomer", "InsertDate");
            AddPrimaryKey("dbo.VendorsCustomer", "Id");
            RenameIndex(table: "dbo.VendorsCustomer", name: "IX_CustomerID", newName: "IX_Id");
            RenameColumn(table: "dbo.VendorsCustomer", name: "CustomerID", newName: "Id");
            AddColumn("dbo.VendorsCustomer", "CustomerID", c => c.Int(nullable: false));
            AddForeignKey("dbo.VendorsCustomer", "Id", "dbo.Customer", "Id");
        }
    }
}
