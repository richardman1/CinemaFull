namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class SubscriberUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscriber", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriber", "Name", c => c.String(nullable: false));
        }
    }
}
