namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class TariffEnglish3 : DbMigration
    {
        
        public override void Up()
        {
            AlterColumn("dbo.Tariff", "EnglishName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tariff", "EnglishName", c => c.String());
        }
    }
}
