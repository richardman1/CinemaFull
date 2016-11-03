using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IVH7_Cinema.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.Domain.Concrete {
    [ExcludeFromCodeCoverage]
    public class EFDbContext : DbContext {

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Screen> Screens { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Show> Shows { get; set; }

        public DbSet<Tariff> Tariffs { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Movie>().HasMany(m => m.Genres).WithMany(g => g.Movies).Map(mc => { mc.MapLeftKey("MovieID"); mc.MapRightKey("GenreID"); mc.ToTable("MovieGenre"); } );

            modelBuilder.Entity<Movie>().HasMany(m => m.Languages).WithMany(l => l.Movies).Map(mc => { mc.MapLeftKey("MovieID"); mc.MapRightKey("LanguageID"); mc.ToTable("MovieLanguage"); });

            modelBuilder.Entity<Movie>().HasMany(m => m.Ratings).WithMany(r => r.Movies).Map(mc => { mc.MapLeftKey("MovieID"); mc.MapRightKey("RatingID"); mc.ToTable("MovieRating"); });

            modelBuilder.Entity<Cinema>().HasMany(c => c.Screens).WithMany(s => s.Cinemas).Map(mc => { mc.MapLeftKey("CinemaID"); mc.MapRightKey("ScreenID"); mc.ToTable("CinemaScreen"); });

            modelBuilder.Entity<Cinema>().HasMany(c => c.Movies).WithMany(m => m.Cinemas).Map(mc => { mc.MapLeftKey("CinemaID"); mc.MapRightKey("MovieID"); mc.ToTable("CinemaMovie"); });

            //modelBuilder.Entity<Ticket>()
            //    .HasRequired(t => t.Show)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }
    }
}
