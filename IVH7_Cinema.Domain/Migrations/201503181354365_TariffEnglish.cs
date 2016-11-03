namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class TariffEnglish : DbMigration
    {
        
        public override void Up()
        {
            AddColumn("dbo.Tariff", "NameEN", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tariff", "NameEN");
        }
    }
}
