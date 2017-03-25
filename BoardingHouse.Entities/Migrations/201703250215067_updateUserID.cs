namespace BoardingHouse.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AuditLog", "UserID", c => c.String());
            AlterColumn("dbo.PaymentCard", "UserID", c => c.String());
            AlterColumn("dbo.Room", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Room", "UserID", c => c.Int());
            AlterColumn("dbo.PaymentCard", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.AuditLog", "UserID", c => c.Int(nullable: false));
        }
    }
}
