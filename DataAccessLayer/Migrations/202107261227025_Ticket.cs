namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ticket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 50),
                        CategoryStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(maxLength: 50),
                        ProductColor = c.String(),
                        ProductSize = c.String(),
                        ProductImagePath = c.String(maxLength: 300),
                        ProductPrice = c.Int(nullable: false),
                        ProductGender = c.String(maxLength: 30),
                        ProductDescription = c.String(maxLength: 100),
                        ProductStatus = c.Boolean(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                        StockID = c.Int(nullable: false),
                        Stock_StockID = c.Int(),
                        Stock_StockID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.Stock_StockID)
                .ForeignKey("dbo.Stocks", t => t.Stock_StockID1)
                .Index(t => t.CategoryID)
                .Index(t => t.OrderID)
                .Index(t => t.Stock_StockID)
                .Index(t => t.Stock_StockID1);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderNumber = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        TotalOrderPrice = c.Int(nullable: false),
                        PaymnetID = c.Int(nullable: false),
                        ProfileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Payments", t => t.PaymnetID, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileID, cascadeDelete: true)
                .Index(t => t.PaymnetID)
                .Index(t => t.ProfileID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymnetID = c.Int(nullable: false, identity: true),
                        PaymnetType = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.PaymnetID);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ProfileID = c.Int(nullable: false, identity: true),
                        ProfileNick = c.String(maxLength: 20),
                        ProfileName = c.String(maxLength: 50),
                        ProfileSurname = c.String(maxLength: 50),
                        ProfileMail = c.String(maxLength: 100),
                        ProfilePhone = c.String(maxLength: 11),
                        ProfileAdress = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ProfileID);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        StockQuantity = c.Int(nullable: false),
                        Product_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.StockID)
                .ForeignKey("dbo.Products", t => t.Product_ProductID)
                .Index(t => t.Product_ProductID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessagesID = c.Int(nullable: false, identity: true),
                        MessagesContext = c.String(maxLength: 250),
                        MessagesCreated_at = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                        TicketID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessagesID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        TicketStatus_StatusID = c.Int(),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.TicketStatus", t => t.TicketStatus_StatusID)
                .Index(t => t.TicketStatus_StatusID);
            
            CreateTable(
                "dbo.TicketStatus",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        StatusTitle = c.String(),
                        StatusDescription = c.String(),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        TickeTypeID = c.Int(nullable: false, identity: true),
                        TicketTitle = c.String(),
                        TicketDescription = c.String(),
                    })
                .PrimaryKey(t => t.TickeTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketStatus_StatusID", "dbo.TicketStatus");
            DropForeignKey("dbo.Stocks", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "Stock_StockID1", "dbo.Stocks");
            DropForeignKey("dbo.Products", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.Orders", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.Products", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "PaymnetID", "dbo.Payments");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Tickets", new[] { "TicketStatus_StatusID" });
            DropIndex("dbo.Stocks", new[] { "Product_ProductID" });
            DropIndex("dbo.Orders", new[] { "ProfileID" });
            DropIndex("dbo.Orders", new[] { "PaymnetID" });
            DropIndex("dbo.Products", new[] { "Stock_StockID1" });
            DropIndex("dbo.Products", new[] { "Stock_StockID" });
            DropIndex("dbo.Products", new[] { "OrderID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropTable("dbo.TicketTypes");
            DropTable("dbo.TicketStatus");
            DropTable("dbo.Tickets");
            DropTable("dbo.Messages");
            DropTable("dbo.Stocks");
            DropTable("dbo.Profiles");
            DropTable("dbo.Payments");
            DropTable("dbo.Orders");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
