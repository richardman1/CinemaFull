namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class AddOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Long(nullable: false),
                        Totaalprijs = c.Double(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.OrderID);
            
            AddColumn("dbo.Ticket", "OrderID", c => c.Long(nullable: false));
            CreateIndex("dbo.Ticket", "OrderID");
            AddForeignKey("dbo.Ticket", "OrderID", "dbo.Order", "OrderID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "OrderID", "dbo.Order");
            DropIndex("dbo.Ticket", new[] { "OrderID" });
            DropColumn("dbo.Ticket", "OrderID");
            DropTable("dbo.Order");
        }
    }
}
