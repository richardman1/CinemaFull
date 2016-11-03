namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    
    [ExcludeFromCodeCoverage]
    public partial class AddBanner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movie", "BannerURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movie", "BannerURL");
        }
    }
}
