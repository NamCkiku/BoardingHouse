namespace BoardingHouse.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.District",
                c => new
                    {
                        districtid = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 255),
                        type = c.String(maxLength: 255),
                        location = c.String(maxLength: 255),
                        provinceid = c.Int(),
                    })
                .PrimaryKey(t => t.districtid);
            
            CreateTable(
                "dbo.Error",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MoreInfomation",
                c => new
                    {
                        MoreInfomationID = c.Int(nullable: false, identity: true),
                        FloorNumber = c.String(),
                        ToiletNumber = c.String(),
                        BedroomNumber = c.String(),
                        Compass = c.String(),
                        ElectricPrice = c.Int(),
                        WaterPrice = c.Int(),
                        Convenient = c.String(storeType: "xml"),
                    })
                .PrimaryKey(t => t.MoreInfomationID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        PostTypeID = c.Int(nullable: false),
                        Image = c.String(maxLength: 256),
                        Description = c.String(maxLength: 500),
                        Content = c.String(),
                        HomeFlag = c.Boolean(nullable: false),
                        HotFlag = c.Boolean(nullable: false),
                        ViewCount = c.Int(),
                        MoreImages = c.String(storeType: "xml"),
                        CreateDate = c.DateTime(),
                        ExpireDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                        Tags = c.String(),
                        CreatedBy = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.PostID);
            
            CreateTable(
                "dbo.PostType",
                c => new
                    {
                        PostTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        Image = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PostTypeID);
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        provinceid = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 255),
                        type = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.provinceid);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        RoomID = c.Int(nullable: false, identity: true),
                        RoomName = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 256),
                        RoomTypeID = c.Int(nullable: false),
                        Image = c.String(nullable: false, maxLength: 256),
                        MoreImages = c.String(storeType: "xml"),
                        WardID = c.Int(),
                        DistrictID = c.Int(),
                        ProvinceID = c.Int(),
                        Acreage = c.Double(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MoreInfomationID = c.Int(),
                        Description = c.String(maxLength: 500),
                        Content = c.String(storeType: "ntext"),
                        CreateDate = c.DateTime(),
                        ExpireDate = c.DateTime(),
                        ViewCount = c.Int(),
                        Status = c.Boolean(nullable: false),
                        Lat = c.Double(),
                        Lng = c.Double(),
                    })
                .PrimaryKey(t => t.RoomID);
            
            CreateTable(
                "dbo.RoomType",
                c => new
                    {
                        RoomTypeID = c.Int(nullable: false, identity: true),
                        RoomTypeName = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        Image = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoomTypeID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.SystemSetting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Field = c.String(maxLength: 250),
                        Value = c.String(maxLength: 250),
                        ValDefault = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ward",
                c => new
                    {
                        wardid = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 255),
                        type = c.String(maxLength: 255),
                        location = c.String(maxLength: 255),
                        districtid = c.Int(),
                    })
                .PrimaryKey(t => t.wardid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ward");
            DropTable("dbo.SystemSetting");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.RoomType");
            DropTable("dbo.Room");
            DropTable("dbo.Province");
            DropTable("dbo.PostType");
            DropTable("dbo.Post");
            DropTable("dbo.MoreInfomation");
            DropTable("dbo.Error");
            DropTable("dbo.District");
        }
    }
}
