using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;
using IVH7_Cinema.Domain.Abstract;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.WebUI.Website.Models;
using System.Net;
using System.Net.Mail;
using System.Globalization;
using System.Threading;
using IVH7_Cinema.WebUI.Website.HtmlHelpers;
using System.Text.RegularExpressions;
using System.Web.Helpers;

namespace IVH7_Cinema.WebUI.Website.Controllers
{
    public class HomeController : BaseController
    {
        private ICinemaRepository Repository;
        private IMailer EMail;

        public int PageSize = 4;
        public String curDate = DateTime.Now.ToShortDateString();

        public HomeController(ICinemaRepository repPar, IMailer mailer)
        {
            Repository = repPar;
            EMail = mailer;

            //set Culture information
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

            ViewBag.NewsError = 0;
            ViewBag.NewsSubscribe = 0;
            ViewBag.ShowID = 0;
            ViewBag.CurrentDate = DateTime.Now.ToShortDateString();
            ViewData["Cinemas"] = Repository.Cinemas.Select(x => new Cinema { CinemaID = x.CinemaID,  Email = x.Email, Phone = x.Phone, Name = x.Name, Address = x.Address, ZipCode = x.ZipCode, City = x.City}).OrderBy(x => x.City).ThenBy(x => x.Name).ToList();
        }
        [ExcludeFromCodeCoverage]
        public ActionResult SetCulture(string culture, string returnurl)
        {
            // Validate input
            System.Diagnostics.Debug.WriteLine(culture);

            culture = CultureHelpers.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnurl);
        }

        public ViewResult Index()
        {
            List<Movie> SpotLightMovies = new List<Movie>();
            List<Movie> TopMovies = new List<Movie>();
            List<Movie> ComingSoonMovies = new List<Movie>();

            SpotLightMovies = Repository.Movies.Where(x => x.MovieID == 11 || x.MovieID == 5 || x.MovieID == 15).ToList();
            TopMovies = Repository.Movies.OrderBy(x => x.ImdbRating).Take(3).ToList();
            ComingSoonMovies = Repository.Movies.Where(x => x.ReleaseDate > DateTime.Today.AddDays(5)).Take(3).ToList();

            HomepageListViewModel model = new HomepageListViewModel
            {
                SpotLightMovies = SpotLightMovies,

                TopMovies = TopMovies,

                ComingSoonMovies = ComingSoonMovies

            };

            return View("Index", model);
        }

        public ViewResult Cinema(String cinema, String Id = "")
        {
            List<Show> ShowList = new List<Show>(); //
            List<Movie> MovieList = new List<Movie>();

            // if date string from url is empty
            if (Id == "") 
            {
                MovieList = Repository.GetDayMovies(DateTime.Today.ToString("d-M-yyyy", new CultureInfo("nl-NL"))).ToList();
            } 
            else 
            {
                MovieList = Repository.GetDayMovies(Id).ToList();
            }

            System.Diagnostics.Debug.WriteLine("HomeController - Cinema - MovieList = " + MovieList.Count);
            
            // Voor elke movie haal ik alle shows op voor een specifieke dag (vandaag of Id).
            foreach (Movie m in MovieList)
            {
                System.Diagnostics.Debug.WriteLine(m.Title);

                if (Id == "")
                {
                    foreach (Show s in Repository.GetMovieShows(cinema, m, DateTime.Today.ToString("d-M-yyyy", new CultureInfo("nl-NL"))).ToList())
                    {
                        ShowList.Add(s);
                        System.Diagnostics.Debug.WriteLine(s.DateTime.ToString("d-M-yyyy", new CultureInfo("nl-NL")));
                    }
                }
                else
                {
                    foreach (Show s in Repository.GetMovieShows(cinema, m, Id).ToList())
                    {
                        ShowList.Add(s);
                        System.Diagnostics.Debug.WriteLine(s.DateTime.ToString("d-M-yyyy", new CultureInfo("nl-NL")));
                    }
                }

            }

            System.Diagnostics.Debug.WriteLine("Cinema: " + cinema);
            List<Movie> MovieListCinema = new List<Movie>();
            foreach(Movie mv in MovieList) {
                System.Diagnostics.Debug.WriteLine("HomeController - Cinema - Aantal Cinemas per Movie: " + mv.Cinemas.Count);
                foreach(Cinema cn in mv.Cinemas) {
                    System.Diagnostics.Debug.WriteLine("Cinema: " + cn.Name);
                    if(cn.Name.Equals(cinema)) {
                        MovieListCinema.Add(mv);
                    }
                }
            }

            foreach(Movie movie in MovieListCinema) {
                System.Diagnostics.Debug.WriteLine("ID: " + movie.MovieID);
            }

            ShowsListViewModel model = new ShowsListViewModel
            {
                Movies = MovieListCinema,

                Shows = ShowList.Where(x => x.Cinema.Name.Equals(cinema)),

                AllMovies = Repository.Movies

            };

            Cinema c = Repository.GetCinema(cinema);
            ViewBag.Cinema = c.Name;
            ViewBag.Address = c.Address;
            ViewBag.ZipCode = c.ZipCode;
            ViewBag.City = c.City;

            return View("Cinema", model);
        }

        public ViewResult Order(Int64 ShowID)
        { 
            ViewBag.Tariffs = Repository.Tariffs;
            Show show = null;
            System.Diagnostics.Debug.WriteLine(ShowID);
            show = Repository.Shows.Where(x => x.ShowID == ShowID).First();

            /*foreach(Genre g in show.Movie.Genres) {
                System.Diagnostics.Debug.WriteLine("Genre = " + g.Name);
            }*/

            /*foreach (Language l in show.Movie.Languages)
            {
                System.Diagnostics.Debug.WriteLine("Language = " + l.LanguageName);
            }*/

            if (show.Movie.Genres.Count == 0 || show.Movie.Languages.Count == 0)
            {
                return View("Index");
            }

            return View("Order", show);
        }

        public ViewResult Movie(Int32 movieID)
        {
            Movie movie = null;
            movie = Repository.GetMovie(movieID);

            List<Show> ShowList = new List<Show>();

            //List<Show> shows = Repository.Shows.Where(s => s.Movie.MovieID == movieID && s.DateTime > DateTime.Today).ToList();

            ShowList = Repository.GetMovieShowsWeek(movie).ToList();

            if (ShowList.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("Shows is null");
                ShowList.Add(new Show { Movie = Repository.GetMovie(movieID) });
            }

            return View("Movie", ShowList);
        }

        public ViewResult FilmWeekOverview()
        {
            return View(Repository.GetMovieWeekMovies());
        }

        public ViewResult MovieOverview()
        {
            return View(Repository.GetMoviesFromNowMovies());
        }

        [HttpGet]
        public ViewResult NewsLetter()
        {
            return View("NewsLetter");
        }


        [HttpGet]
        public ViewResult AddSubscriber()
        {
            return View("NewsLetter");
        }

        [HttpPost]
        public ViewResult AddSubscriber(Subscriber subscriber)
        {
            bool x = false;
            if (ModelState.IsValid)
            {
                foreach(Subscriber r in Repository.Subscribers){
                    if(r.Email.Equals(subscriber.Email)){
                        x = true;
                        break;
                    }
                    else
                    {
                        x = false;
                    }
                }
                if(x == false){
                    Repository.AddSubscriber(subscriber);

                    try
                    {
                        WebMail.SmtpServer = "smtp.gmail.com";
                        WebMail.SmtpPort = 587;
                        WebMail.EnableSsl = true;
                        WebMail.UserName = "richarddejongh1995@gmail.com";
                        WebMail.Password = "Rambogek1!";
                        WebMail.From = "noreply@CinemA.com";

                        WebMail.Send(subscriber.Email, "Aanmelding nieuwsbrief CinemA", subscriber.Name + ", u bent succesvol aangemeld voor de nieuwsbrief van CinemA");

                    }
                    catch (Exception)
                    {
                        System.Diagnostics.Debug.WriteLine("Er is een fout opgetreden tijdens het versturen van een mail naar uw eigen mail. Probeer het nogmaals.");
                    }

                    @ViewBag.NewsSubscribe = 1;
                    return View("NewsLetter");
                }
                else
                {
                    @ViewBag.NewsSubscribe = 2;
                    return View("NewsLetter");
                }
            }
            else
            {
                return View("NewsLetter");
            }
        }

        [HttpPost]
        public ViewResult DeleteSubscriber(String email2)
        {
            if(email2 != ""){
                if (!Regex.IsMatch(email2, ".+@.+\\..+", RegexOptions.IgnoreCase))
                {
                    @ViewBag.NewsError = 4;
                    return View("NewsLetter");
                }
                else
                {
                    Subscriber s = null;
                    foreach (Subscriber r in Repository.Subscribers.ToList<Subscriber>())
                    {
                        if (r.Email.Equals(email2))
                        {
                            s = r;
                        }
                    }
                    if (s != null)
                    {
                        @ViewBag.NewsError = 1;
                        Repository.DeleteSubscriber(s);
                        //return View("SubscriberDeleted", s);
                        return View("NewsLetter");
                    }
                    else
                    {
                        @ViewBag.NewsError = 2;
                        @ViewBag.email = email2;
                        //return View("SubscriberNotExisting");
                        return View("NewsLetter");
                    }
                }
                

            }
            else{
                @ViewBag.NewsError = 3;
                return View("NewsLetter");
            }
        }
    }
}