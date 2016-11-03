namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class TariffEnglish2 : DbMigration
    {
        
        public override void Up()
        {
            AddColumn("dbo.Tariff", "EnglishName", c => c.String());
            DropColumn("dbo.Tariff", "NameEN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tariff", "NameEN", c => c.String());
            DropColumn("dbo.Tariff", "EnglishName");
        }
    }
}
