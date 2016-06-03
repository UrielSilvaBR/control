namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_pedidos : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Order", "PaymentTermID", c => c.Int());
            AddColumn("dbo.OrderProduct", "DeadlineItem", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "PaymentTermID");
            AddForeignKey("dbo.Order", "PaymentTermID", "dbo.PaymentTerm", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "PaymentTermID", "dbo.PaymentTerm");
            DropIndex("dbo.Order", new[] { "PaymentTermID" });
            DropColumn("dbo.OrderProduct", "DeadlineItem");
            DropColumn("dbo.Order", "PaymentTermID");
            DropTable("dbo.PaymentTerm");
        }
    }
}
