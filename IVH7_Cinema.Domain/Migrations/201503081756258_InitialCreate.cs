namespace IVH7_Cinema.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinema",
                c => new
                    {
                        CinemaID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CinemaID);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        MovieID = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Duration = c.Long(nullable: false),
                        Is3DAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MovieID);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        GenreID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        LanguageID = c.Long(nullable: false, identity: true),
                        LanguageCode = c.String(),
                        LanguageName = c.String(),
                    })
                .PrimaryKey(t => t.LanguageID);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        RatingID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.RatingID);
            
            CreateTable(
                "dbo.Screen",
                c => new
                    {
                        ScreenID = c.Long(nullable: false),
                        Size = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ScreenID);
            
            CreateTable(
                "dbo.Seat",
                c => new
                    {
                        SeatID = c.Long(nullable: false, identity: true),
                        SeatNumber = c.Long(nullable: false),
                        RowNumber = c.Long(nullable: false),
                        ScreenID = c.Long(nullable: false),
                        Vacated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SeatID)
                .ForeignKey("dbo.Screen", t => t.ScreenID, cascadeDelete: true)
                .Index(t => t.ScreenID);
            
            CreateTable(
                "dbo.Show",
                c => new
                    {
                        ShowID = c.Long(nullable: false, identity: true),
                        MovieID = c.Long(nullable: false),
                        ScreenID = c.Long(nullable: false),
                        CinemaID = c.Long(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        LanguageID = c.Long(nullable: false),
                        Subtitles = c.String(),
                        Is3D = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ShowID)
                .ForeignKey("dbo.Cinema", t => t.CinemaID, cascadeDelete: true)
                .ForeignKey("dbo.Language", t => t.LanguageID, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Screen", t => t.ScreenID, cascadeDelete: true)
                .Index(t => t.MovieID)
                .Index(t => t.ScreenID)
                .Index(t => t.CinemaID)
                .Index(t => t.LanguageID);
            
            CreateTable(
                "dbo.Tariff",
                c => new
                    {
                        TariffID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TariffID);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        TicketID = c.Long(nullable: false, identity: true),
                        ShowID = c.Long(nullable: false),
                        TariffID = c.Long(nullable: false),
                        SeatID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Seat", t => t.SeatID, cascadeDelete: true)
                .ForeignKey("dbo.Show", t => t.ShowID, cascadeDelete: false)
                .ForeignKey("dbo.Tariff", t => t.TariffID, cascadeDelete: true)
                .Index(t => t.ShowID)
                .Index(t => t.TariffID)
                .Index(t => t.SeatID);
            
            CreateTable(
                "dbo.MovieGenre",
                c => new
                    {
                        MovieID = c.Long(nullable: false),
                        GenreID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieID, t.GenreID })
                .ForeignKey("dbo.Movie", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.GenreID, cascadeDelete: true)
                .Index(t => t.MovieID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.MovieLanguage",
                c => new
                    {
                        MovieID = c.Long(nullable: false),
                        LanguageID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieID, t.LanguageID })
                .ForeignKey("dbo.Movie", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Language", t => t.LanguageID, cascadeDelete: true)
                .Index(t => t.MovieID)
                .Index(t => t.LanguageID);
            
            CreateTable(
                "dbo.MovieRating",
                c => new
                    {
                        MovieID = c.Long(nullable: false),
                        RatingID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieID, t.RatingID })
                .ForeignKey("dbo.Movie", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Rating", t => t.RatingID, cascadeDelete: true)
                .Index(t => t.MovieID)
                .Index(t => t.RatingID);
            
            CreateTable(
                "dbo.CinemaMovie",
                c => new
                    {
                        CinemaID = c.Long(nullable: false),
                        MovieID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.CinemaID, t.MovieID })
                .ForeignKey("dbo.Cinema", t => t.CinemaID, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.CinemaID)
                .Index(t => t.MovieID);
            
            CreateTable(
                "dbo.CinemaScreen",
                c => new
                    {
                        CinemaID = c.Long(nullable: false),
                        ScreenID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.CinemaID, t.ScreenID })
                .ForeignKey("dbo.Cinema", t => t.CinemaID, cascadeDelete: true)
                .ForeignKey("dbo.Screen", t => t.ScreenID, cascadeDelete: true)
                .Index(t => t.CinemaID)
                .Index(t => t.ScreenID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "TariffID", "dbo.Tariff");
            DropForeignKey("dbo.Ticket", "ShowID", "dbo.Show");
            DropForeignKey("dbo.Ticket", "SeatID", "dbo.Seat");
            DropForeignKey("dbo.Show", "ScreenID", "dbo.Screen");
            DropForeignKey("dbo.Show", "MovieID", "dbo.Movie");
            DropForeignKey("dbo.Show", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.Show", "CinemaID", "dbo.Cinema");
            DropForeignKey("dbo.CinemaScreen", "ScreenID", "dbo.Screen");
            DropForeignKey("dbo.CinemaScreen", "CinemaID", "dbo.Cinema");
            DropForeignKey("dbo.Seat", "ScreenID", "dbo.Screen");
            DropForeignKey("dbo.CinemaMovie", "MovieID", "dbo.Movie");
            DropForeignKey("dbo.CinemaMovie", "CinemaID", "dbo.Cinema");
            DropForeignKey("dbo.MovieRating", "RatingID", "dbo.Rating");
            DropForeignKey("dbo.MovieRating", "MovieID", "dbo.Movie");
            DropForeignKey("dbo.MovieLanguage", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.MovieLanguage", "MovieID", "dbo.Movie");
            DropForeignKey("dbo.MovieGenre", "GenreID", "dbo.Genre");
            DropForeignKey("dbo.MovieGenre", "MovieID", "dbo.Movie");
            DropIndex("dbo.CinemaScreen", new[] { "ScreenID" });
            DropIndex("dbo.CinemaScreen", new[] { "CinemaID" });
            DropIndex("dbo.CinemaMovie", new[] { "MovieID" });
            DropIndex("dbo.CinemaMovie", new[] { "CinemaID" });
            DropIndex("dbo.MovieRating", new[] { "RatingID" });
            DropIndex("dbo.MovieRating", new[] { "MovieID" });
            DropIndex("dbo.MovieLanguage", new[] { "LanguageID" });
            DropIndex("dbo.MovieLanguage", new[] { "MovieID" });
            DropIndex("dbo.MovieGenre", new[] { "GenreID" });
            DropIndex("dbo.MovieGenre", new[] { "MovieID" });
            DropIndex("dbo.Ticket", new[] { "SeatID" });
            DropIndex("dbo.Ticket", new[] { "TariffID" });
            DropIndex("dbo.Ticket", new[] { "ShowID" });
            DropIndex("dbo.Show", new[] { "LanguageID" });
            DropIndex("dbo.Show", new[] { "CinemaID" });
            DropIndex("dbo.Show", new[] { "ScreenID" });
            DropIndex("dbo.Show", new[] { "MovieID" });
            DropIndex("dbo.Seat", new[] { "ScreenID" });
            DropTable("dbo.CinemaScreen");
            DropTable("dbo.CinemaMovie");
            DropTable("dbo.MovieRating");
            DropTable("dbo.MovieLanguage");
            DropTable("dbo.MovieGenre");
            DropTable("dbo.Ticket");
            DropTable("dbo.Tariff");
            DropTable("dbo.Show");
            DropTable("dbo.Seat");
            DropTable("dbo.Screen");
            DropTable("dbo.Rating");
            DropTable("dbo.Language");
            DropTable("dbo.Genre");
            DropTable("dbo.Movie");
            DropTable("dbo.Cinema");
        }
    }
}
