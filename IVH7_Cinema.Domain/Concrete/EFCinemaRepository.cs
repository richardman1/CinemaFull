using IVH7_Cinema.Domain.Abstract;
using IVH7_Cinema.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace IVH7_Cinema.Domain.Concrete
{
    [ExcludeFromCodeCoverage]
    public class EFCinemaRepository : ICinemaRepository
    {
        private EFDbContext Context = new EFDbContext();

        public EFCinemaRepository(EFDbContext Context)
        {
            this.Context = Context;
        }

        public IEnumerable<Movie> Movies
        {
            get { return Context.Movies.Include(x => x.Genres).ToList(); }
        }

        //Get functionality
        public Movie GetMovie(Int64 iD)
        {
            return Context.Movies.Where(x => x.MovieID == iD).Include(x => x.Ratings).FirstOrDefault();
            //List<Movie> movies = Movies.ToList<Movie>();
            //foreach (Movie m in movies)
            //{
            //    if (m.Title.Equals(title))
            //    {
            //        return m;
            //    }
            //}
            //return null;
        }

        //Get functionality
        public Order GetOrder(Int64 iD)
        {
            return Context.Orders.Where(x => x.OrderID == iD).Include(x => x.Tickets).FirstOrDefault();
        }

        public void ChangeOrderStatus(Int64 orderID, string status)
        {
            Context.Orders.Where(x => x.OrderID == orderID).First().Status = status;
            Context.SaveChanges();
        }

        public void DeleteOrder(Int64 orderID)
        {
            Context.Orders.Remove(Orders.Where(x => x.OrderID == orderID).First());
            Context.SaveChanges();
        }

        public Screen GetScreen(Int64 number)
        {
            return Context.Screens.Where(x => x.ScreenID == number).Include(x => x.Seats).FirstOrDefault();
            //List<Screen> screens = Screens.ToList<Screen>();
            //foreach (Screen m in screens)
            //{
            //    if (m.Number.Equals(number))
            //    {
            //        return m;
            //    }
            //}
            //return null;
        }

        //Add functionality
        public void AddMovies(Movie movie)
        {
            Context.Movies.Add(movie);
            Context.SaveChanges();
        }

        public void AddLanguage(Language lang)
        {
            Context.Languages.Add(lang);
            Context.SaveChanges();
        }

        public IEnumerable<Cinema> Cinemas
        {
            get { return Context.Cinemas; }
        }

        //Get functionality
        public Cinema GetCinema(string name)
        {
            return Context.Cinemas.Where(x => x.Name == name).FirstOrDefault();
        }

        public IEnumerable<Language> Languages
        {
            get { return Context.Languages; }
        }

        public IEnumerable<Genre> Genres
        {
            get { return Context.Genres; }
        }

        public IEnumerable<Rating> Ratings
        {
            get { return Context.Ratings; }
        }

        public IEnumerable<Subscriber> Subscribers
        {
            get { return Context.Subscribers; }
        }

        //Add functionality
        public void AddCinemas(Cinema cinema)
        {
            Context.Cinemas.Add(cinema);
            Context.SaveChanges();
        }

        public void AddSubscriber(Subscriber subscriber)
        {
            Context.Subscribers.Add(subscriber);
            Context.SaveChanges();
        }

        public void DeleteSubscriber(Subscriber subscriber)
        {
            Context.Subscribers.Remove(subscriber);
            Context.SaveChanges();
        }

        public IEnumerable<Screen> Screens
        {
            get
            {
                //IEnumerable<Seat> seats = Context.Seats;
                //List<Seat> seatsList = seats.ToList();

                //foreach (Screen s in Context.Screens)
                //{
                //    s.Seats = seatsList.Where(x => x.ScreenID == s.Number).ToList();

                //    foreach (Seat b in seatsList)
                //    {
                //        System.Diagnostics.Debug.WriteLine("Het ID is: " + b.SeatID);
                //    }
                //    //s.Seats = Context.Seats.Where(x => x.ScreenID == s.Number);
                //    //s.Seats = new Seat[] { new Seat { SeatNumber = 1}, new Seat { SeatNumber = 2} };
                //    System.Diagnostics.Debug.WriteLine(s.Seats.Count);
                //}
                //return Context.Screens;
                return Context.Screens.Include(x => x.Seats);
            }
        }
        
        //Add functionality
        public void AddScreens(Screen screen)
        {
            Context.Screens.Add(screen);
            Context.SaveChanges();
        }

        public IEnumerable<Seat> Seats
        {
            get { return Context.Seats; }
        }

        //Add functionality
        public void AddSeats(Seat seat)
        {
            Context.Seats.Add(seat);
            Context.SaveChanges();
        }

        public IEnumerable<Show> Shows
        {
            get
            {
                return Context.Shows.Include(x => x.Movie).Include(x => x.Screen).Include(x=> x.Screen.Seats).Include(x => x.Tickets);
                //return Context.Shows;
            }
        }

        public void AddShows(Show show)
        {
            Context.Shows.Add(show);
            Context.SaveChanges();
        }

        public IEnumerable<Tariff> Tariffs
        {
            get { return Context.Tariffs; }
        }

        public Tariff GetTariff(string name)
        {
            return Tariffs.Where(x => x.Name.Equals(name)).First();
        }

        public void AddTariffs(Tariff tariff)
        {
            Context.Tariffs.Add(tariff);
            Context.SaveChanges();
        }

        public IEnumerable<Ticket> Tickets
        {
            get { return Context.Tickets; }
        }

        //Add functionality
        public void AddTickets(Ticket ticket)
        {
            Context.Tickets.Add(ticket);
            Context.SaveChanges();
        }

        //Add functionality
        public void AddRating(Rating rating)
        {
            Context.Ratings.Add(rating);
            Context.SaveChanges();
        }
        // aan te passen
        //public void SetVacated(IEnumerable<Seat> unVacatedSeats, Show show)
        //{
        //    Context.Seats.Find(unVacatedSeats).Vacated = true;
        //}

        //public void SetUnVacated(IEnumerable<Seat> vacatedSeats, Show show)
        //{
        //    Context.Seats.Find(vacatedSeats).Vacated = true;
        //}
        
        public IEnumerable<Order> Orders
        {
            get { return Context.Orders; }
        }

        public void AddOrder(Order order)
        {
            Context.Orders.Add(order);
            Context.SaveChanges();
        }
        public IEnumerable<Movie> GetMovieWeekMovies()
        {   
            DateTime Today =  DateTime.Now;
            //Get the next Thursday
            DateTime NextThursday = Today.Date.AddDays(((int)Today.DayOfWeek - (int)DayOfWeek.Thursday) + 7);
            //Get the previous Thursday
            DateTime PreviousThursday = NextThursday.AddDays(-7);
            return Shows.Where(x => x.DateTime < NextThursday && x.DateTime >= PreviousThursday).Select(x => x.Movie).ToList<Movie>().Distinct();
        }

        public IEnumerable<Movie> GetMoviesFromNowMovies()
        {
            List<Movie> MoviesToBeReleased = Movies.Where(x=>x.ReleaseDate > DateTime.Today).ToList();
            List<Movie> MoviesWithShowsThisWeek = GetMovieWeekMovies().ToList();
            return MoviesWithShowsThisWeek.Concat(MoviesToBeReleased);
        }

        // Returns all unique movies for a certain date
        public IEnumerable<Movie> GetDayMovies(String date)
        {
            return Shows.Where(x => x.DateTime.ToString("d-M-yyyy", new CultureInfo("nl-NL")) == date && x.DateTime >= DateTime.Now).Select(x => x.Movie).ToList<Movie>().Distinct();
        }

        // Returns all shows for a movie and cinema on a certain date
        public List<Show> GetMovieShows(String cinema, Movie movie, String date)
        {
            DateTime Today = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("Duurt Lang");
            List<Show> shows = Context.Shows.Where(x => x.Cinema.Name == cinema && x.MovieID == movie.MovieID && x.DateTime >= Today).ToList();
            return shows.Where(x => x.DateTime.ToString("d-M-yyyy", new CultureInfo("nl-NL")) == date).ToList();
        }

        public List<Show> GetMovieShowsWeek(Movie movie)
        {
            DateTime Today = DateTime.Now;
            //Get the next Thursday
            DateTime NextThursday = Today.Date.AddDays(((int)Today.DayOfWeek - (int)DayOfWeek.Thursday) + 7);

            return Shows.Where(x => x.DateTime >= DateTime.Today && x.DateTime < NextThursday && x.DateTime >= DateTime.Now && x.MovieID == movie.MovieID).ToList();
        }
    }
}