using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using IVH7_Cinema.WebUI.Website.Controllers;
using IVH7_Cinema.WebUI.Website.Models;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.Domain.Abstract;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using System.Globalization;

namespace IVH7_Cinema.UnitTests.Website
{
    [TestClass][ExcludeFromCodeCoverage]
    public class HomeControllerTests
    {
        //Mock repository
        Mock<ICinemaRepository> RepMock = new Mock<ICinemaRepository>();
        Mock<IMailer> MailMock = new Mock<IMailer>();

        //spullen voor in de mock
        List<Movie> Movies = new List<Movie>
                {
                    new Movie { MovieID = 1, Title = "Gooise Vrouwen", Duration = 120, Genres = new List<Genre> { new Genre { Name = "Women" } }, Is3DAvailable = false},
                    new Movie { MovieID = 2, Title = "50 shades of grey", Duration = 130, Genres = new List<Genre> { new Genre { Name = "Erotic" } }, Is3DAvailable = false },
                    new Movie { MovieID = 3,  Title = "Imitation Game", Duration = 110, Genres = new List<Genre> { new Genre { Name = "Action" } }, Is3DAvailable = true }
                };
        List<Cinema> Cinemas = new List<Cinema>{
            new Cinema { CinemaID = 1, Name = "CinemA", City = "Breda"},
            new Cinema { CinemaID = 2, Name = "CinemB", City = "Tilburg"}
        };
        List<Show> Shows = new List<Show>{
            new Show { ShowID = 1, Movie =  new Movie { MovieID = 1, Title = "Gooise Vrouwen", Duration = 120, Genres = new List<Genre> { new Genre { Name = "Women" } }, Is3DAvailable = false, Languages = new Language[] { new Language() { LanguageID = 1, LanguageName = "Engels"}}}, MovieID = 1, CinemaID = 1, ScreenID = 1, DateTime = DateTime.Today,Language = new Language {LanguageID = 1, LanguageName = "Nederlands"}, LanguageID = 1 },
            new Show { ShowID = 2, Movie = new Movie { MovieID = 2, Title = "50 shades of grey", Duration = 130, Genres = new List<Genre> { new Genre { Name = "Erotic" } }, Is3DAvailable = false }, MovieID = 2, CinemaID = 1, ScreenID = 1, DateTime = DateTime.Today,Language = new Language {LanguageID = 1, LanguageName = "Nederlands"}, LanguageID = 1 }
        };

        List<Subscriber> Subscribers = new List<Subscriber> { new Subscriber { Name = "Subscriber 1", Email = "1@1.com"},
                                                              new Subscriber { Name = "Subscriber 2", Email = "2@2.com"},
                                                              new Subscriber { Name = "Subscriber 3", Email = "3@3.com"}};

        
        




        [TestMethod]
        public void IndexViewTest()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);

            // Act - call the action method
            ViewResult result = target.Index();

            // Assert - check the result
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(HomepageListViewModel));
        }

        [TestMethod]
        public void CinemaViewTest()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);

            RepMock.Setup(x => x.GetCinema("Cinema")).Returns(Cinemas[0]);
            

            // Act - call the action method
            ViewResult result = target.Cinema("Cinema");

            // Assert - check the result
            Assert.AreEqual("Cinema", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ShowsListViewModel));
        }

        [TestMethod]
        public void OrderTest()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);
            Int64 ShowID = 1;
            RepMock.Setup(x => x.Shows).Returns(Shows);

            // Act - call the action method
            ViewResult result = target.Order(ShowID);

            // Assert - check the result
            Assert.AreEqual("Order", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Show));
        }

        [TestMethod]
        public void MovieTest()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);
            Int32 MovieID = 1;
            RepMock.Setup(x => x.GetMovie(MovieID)).Returns(Movies[0]);
            RepMock.Setup(x => x.GetMovieShowsWeek(Movies[0])).Returns(Shows);

            // Act - call the action method
            ViewResult result = target.Movie(MovieID);

            // Assert - check the result
            Assert.AreEqual("Movie", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<Show>));
        }

        [TestMethod]
        public void FilmWeekTest()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);
            RepMock.Setup(x => x.GetMovieWeekMovies()).Returns(Movies);

            // Act - call the action method
            ViewResult result = target.FilmWeekOverview();

            // Assert - check the result
            Assert.AreEqual("", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<Movie>));
        }

        [TestMethod]
        public void MovieOverview()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);
            RepMock.Setup(x => x.GetMoviesFromNowMovies()).Returns(Movies);

            // Act - call the action method
            ViewResult result = target.MovieOverview();

            // Assert - check the result
            Assert.AreEqual("", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<Movie>));
        }

        [TestMethod]
        public void NewsletterViewTest()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);

            // Act - call the action method
            ViewResult result = target.NewsLetter();

            // Assert - check the result
            Assert.AreEqual("NewsLetter", result.ViewName);
        }

        [TestMethod]
        public void AddSubscriberExistsTest()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);
            RepMock.Setup(x => x.Subscribers).Returns(Subscribers);

            // Act - call the action method
            ViewResult result = target.AddSubscriber(Subscribers[0]);

            // Assert - check the result
            //Assert.AreEqual("SubscriberExists", result.ViewName);
            Assert.AreEqual(2, result.ViewData["NewsSubscribe"]);
            //Assert.IsInstanceOfType(result.ViewData.Model, typeof(Subscriber));
        }

        [TestMethod]
        public void AddSubscriberNotExistsTest()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);
            RepMock.Setup(x => x.Subscribers).Returns(Subscribers);


            // Act - call the action method
            ViewResult result = target.AddSubscriber(new Subscriber { Name = "5", Email = "2@2.nl"});

            // Assert - check the result
            Assert.AreEqual(1, result.ViewData["NewsSubscribe"]);
            //Assert.AreEqual("SubscriberAdded", result.ViewName);
            //Assert.IsInstanceOfType(result.ViewData.Model, typeof(Subscriber));
        }
        [TestMethod]
        public void AddSubscriberEmptyTest()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);
            RepMock.Setup(x => x.Subscribers).Returns(Subscribers);


            // Act - call the action method
            ViewResult result = target.AddSubscriber();

            // Assert - check the result
            Assert.AreEqual("NewsLetter", result.ViewName);
        }

        [TestMethod]
        public void DeleteSubscriberExists()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);
            RepMock.Setup(x => x.Subscribers).Returns(Subscribers);


            // Act - call the action method
            ViewResult result = target.DeleteSubscriber(Subscribers[0].Email);

            // Assert - check the result
            Assert.AreEqual(1, result.ViewData["NewsError"]);
            //Assert.AreEqual("SubscriberDeleted", result.ViewName);
            //Assert.IsInstanceOfType(result.ViewData.Model, typeof(Subscriber));
        }

        [TestMethod]
        public void DeleteSubscriberNotExists()
        {
            // Arrange - create the controller
            HomeController target = new HomeController(RepMock.Object, MailMock.Object);
            RepMock.Setup(x => x.Subscribers).Returns(Subscribers);


            // Act - call the action method
            ViewResult result = target.DeleteSubscriber("test");

            // Assert - check the result
            Assert.AreEqual(4, result.ViewData["NewsError"]);
            //Assert.AreEqual("SubscriberNotExisting", result.ViewName);
        }
        //[TestMethod]
        //public void DeleteSubscriberNoMail()
        //{
        //    // Arrange - create the controller
        //    HomeController target = new HomeController(RepMock.Object, MailMock.Object);
        //    RepMock.Setup(x => x.Subscribers).Returns(Subscribers);


        //    // Act - call the action method
        //    ViewResult result = target.DeleteSubscriber();

        //    // Assert - check the result
        //    Assert.AreEqual("NewsLetter", result.ViewName);
        //}

    }
}