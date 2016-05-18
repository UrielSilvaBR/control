namespace Control.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NCM_Produto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "NCMCode", c => c.String());
            AddColumn("dbo.Product", "DescriptionNCC", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "DescriptionNCC");
            DropColumn("dbo.Product", "NCMCode");
        }
    }
}
