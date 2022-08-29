namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberOfRentedMoviesInCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "NumberOfRentedMovies", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "NumberOfRentedMovies");
        }
    }
}
