namespace BoardingHouse.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixDb : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.sysdiagrams");
        }
        
        public override void Down()
        {
        }
    }
}
