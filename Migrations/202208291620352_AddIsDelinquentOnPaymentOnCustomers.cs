namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDelinquentOnPaymentOnCustomers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsDelinquentOnPayment", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "IsDelinquentOnPayment");
        }
    }
}
