namespace BoardingHouse.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAudilogandCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLog",
                c => new
                    {
                        AuditLogID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        LogType = c.Int(nullable: false),
                        Description = c.String(),
                        IPAddress = c.String(maxLength: 256),
                        Device = c.String(maxLength: 256),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AuditLogID);
            
            CreateTable(
                "dbo.PaymentCard",
                c => new
                    {
                        PaymentCardID = c.Int(nullable: false, identity: true),
                        ISP = c.String(maxLength: 256),
                        Card = c.String(maxLength: 256),
                        Seri = c.String(maxLength: 256),
                        Status = c.Boolean(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentCardID);
            
            AddColumn("dbo.Room", "FullName", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Room", "Email", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Room", "UserID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Room", "UserID");
            DropColumn("dbo.Room", "Email");
            DropColumn("dbo.Room", "FullName");
            DropTable("dbo.PaymentCard");
            DropTable("dbo.AuditLog");
        }
    }
}
