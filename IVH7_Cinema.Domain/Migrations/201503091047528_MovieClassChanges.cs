namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class MovieClassChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movie", "Description", c => c.String());
            AddColumn("dbo.Movie", "Director", c => c.String());
            AddColumn("dbo.Movie", "ImdbURL", c => c.String());
            AddColumn("dbo.Movie", "ImdbRating", c => c.String());
            AddColumn("dbo.Movie", "TrailerURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movie", "TrailerURL");
            DropColumn("dbo.Movie", "ImdbRating");
            DropColumn("dbo.Movie", "ImdbURL");
            DropColumn("dbo.Movie", "Director");
            DropColumn("dbo.Movie", "Description");
        }
    }
}
