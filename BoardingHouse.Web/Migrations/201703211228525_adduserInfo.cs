namespace BoardingHouse.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Sex", c => c.String(maxLength: 10));
            AddColumn("dbo.AspNetUsers", "Coin", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Point", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Point");
            DropColumn("dbo.AspNetUsers", "Coin");
            DropColumn("dbo.AspNetUsers", "Sex");
        }
    }
}
