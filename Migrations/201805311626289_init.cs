namespace MVCGarage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkedVehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Regnr = c.String(nullable: false, maxLength: 10),
                        Type = c.String(nullable: false, maxLength: 15),
                        Color = c.String(nullable: false, maxLength: 15),
                        Brand = c.String(nullable: false, maxLength: 25),
                        NrofWheels = c.Int(nullable: false),
                        Length = c.Double(nullable: false),
                        FuelType = c.String(nullable: false, maxLength: 15),
                        ParkedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParkedVehicles");
        }
    }
}
