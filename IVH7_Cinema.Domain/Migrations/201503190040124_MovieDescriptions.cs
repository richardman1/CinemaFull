namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class MovieDescriptions : DbMigration
    {

        public override void Up()
        {
            AddColumn("dbo.Movie", "DescriptionEN", c => c.String());
            AddColumn("dbo.Movie", "DescriptionFR", c => c.String());
            AlterColumn("dbo.Tariff", "EnglishName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tariff", "EnglishName", c => c.String(nullable: false));
            DropColumn("dbo.Movie", "DescriptionFR");
            DropColumn("dbo.Movie", "DescriptionEN");
        }
    }
}
