namespace Web_API_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editInPriceOfProdcut : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Price", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Price", c => c.Int(nullable: false));
        }
    }
}
