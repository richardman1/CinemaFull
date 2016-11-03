using System.Collections.Generic;
using IVH7_Cinema.Domain.Entities;
using System;

namespace IVH7_Cinema.Domain.Abstract {
    public interface ICinemaRepository {
        IEnumerable<Movie> Movies { get; }
        IEnumerable<Seat> Seats { get; }
        IEnumerable<Cinema> Cinemas { get; }
        IEnumerable<Screen> Screens { get; }
        IEnumerable<Show> Shows { get; }
        IEnumerable<Tariff> Tariffs { get; }
        IEnumerable<Ticket> Tickets { get; }
        IEnumerable<Rating> Ratings { get; }
        IEnumerable<Language> Languages { get; }
        IEnumerable<Genre> Genres { get; }
        IEnumerable<Order> Orders { get; }
        IEnumerable<Movie> GetMovieWeekMovies();
        IEnumerable<Movie> GetMoviesFromNowMovies();
        IEnumerable<Movie> GetDayMovies(String date);
        List<Show> GetMovieShows(String cinema, Movie movie, String date);
        List<Show> GetMovieShowsWeek(Movie movie);
        IEnumerable<Subscriber> Subscribers { get; }

        void AddRating(Rating rating);
        void AddLanguage(Language language);
        void AddMovies(Movie movie);
        void AddCinemas(Cinema cinema);
        void AddScreens(Screen screen);
        void AddSeats(Seat seat);
        void AddShows(Show show);
        void AddTariffs(Tariff tariff);
        void AddTickets(Ticket ticket);
        void AddOrder(Order order);
        void ChangeOrderStatus(Int64 orderID, string status);
        void AddSubscriber(Subscriber subscriber);
        //void SetVacated(IEnumerable<Seat> unVacatedSeats, Show show);
        //void SetUnVacated(IEnumerable<Seat> VacatedSeats, Show show);

        void DeleteSubscriber(Subscriber subscriber);

        Cinema GetCinema(string name);

        Movie GetMovie(Int64 iD);

        Screen GetScreen(Int64 number);

        Tariff GetTariff(string name);

        Order GetOrder(Int64 iD);

        void DeleteOrder(Int64 orderID);
    }
}
