using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using IVH7_Cinema.Domain.Abstract;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.WebUI.Controllers;
using IVH7_Cinema.WebUI.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace IVH7_Cinema.UnitTests {
    [TestClass][ExcludeFromCodeCoverage]
    public class CinemaTest {

        Show[] testshows = new Show[] {
            new Show() { 
                ShowID = 1, Movie = new Movie() { Title = "Fifty Shades of Grey", Duration = 120, Is3DAvailable = false
                    , Genres = new Genre[] { new Genre() { GenreID = 1, Name = "Actie"}, new Genre() { GenreID = 2, Name = "Horror"}}
                    , Languages = new Language[] { new Language() { LanguageID = 1, LanguageName = "Engels"}}
                }
                , Screen = new Screen() { ScreenID = 1, Size = 120, }, Cinema = new Cinema() { Name = "AvanCity"}
                , Is3D = false , Language = new Language() { LanguageName = "Engels", LanguageCode = "EN" }
                , Subtitles = "Nederlands", DateTime = DateTime.Now },

            new Show() { 
                ShowID = 2, Movie = new Movie() { Title = "Focus", Duration = 100 , Is3DAvailable = true
                    , Genres = new List<Genre>()
                    , Languages = new Language[] { new Language() { LanguageID = 1, LanguageName = "Engels"}} }
                , Screen = new Screen() { ScreenID = 2, Size = 120, }, Cinema = new Cinema() { Name = "AvanCity"}
                , Is3D = false, Language = new Language() { LanguageName = "Engels", LanguageCode = "EN" }
                , Subtitles = "Nederlands", DateTime = DateTime.Now },
                
                new Show() { 
                ShowID = 3, Movie = new Movie() { Title = "Jupiter Ascending", Duration = 150, Is3DAvailable = true}
                , Screen = new Screen() { ScreenID = 3, Size = 120, }, Cinema = new Cinema() { Name = "AvanCity"}
                , Is3D = false, Language = new Language() { LanguageName = "Engels", LanguageCode = "EN" }
                , Subtitles = "Nederlands", DateTime = DateTime.Now },
                
                new Show() { 
                ShowID = 4, Movie = new Movie() { Title = "Gooische Vrouwen 2", Duration = 106, Is3DAvailable = false }
                , Screen = new Screen() { ScreenID = 1, Size = 120, }, Is3D = false
                , Language = new Language() { LanguageName = "Engels", LanguageCode = "EN" }
                , Subtitles = "Nederlands", DateTime = DateTime.Now },
                
                new Show() { 
                ShowID = 5, Movie = new Movie() { Title = "Fifty Shades of Grey", Duration = 120, Is3DAvailable = false }
                , Screen = new Screen() { ScreenID = 1, Size = 120, }, Is3D = false
                , Language = new Language() { LanguageName = "Engels", LanguageCode = "EN" }
                , Subtitles = "Nederlands", DateTime = DateTime.Now }
        };

        [TestMethod]
        public void PinCheckPaid()
        {
            //Arrange
            List<Ticket> Tickets = new List<Ticket> { 
            new Ticket { TicketID = 23,
                Order = new Order { OrderID = 1, Status = "Betaald", Totaalprijs = 25.00 }, 
                Seat = new Seat { RowNumber = 4,  SeatNumber = 6}, 
                Show = new Show { Screen = new Screen { ScreenID = 1, Size = 120 }, Movie = new Movie{ Title = "Big Hero 6"}, Is3D = true, Language = new Language { LanguageName = "Nederlands"}}, 
                Tariff = new Tariff{ Name = "Normaal", Price = 8.00}
            }};

            Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();
            mock.Setup(m => m.Orders).Returns(new List<Order> {
            new Order{OrderID = 1, Status = "Betaald", Totaalprijs = 25.00, Tickets = Tickets}});  
            CinemaController controller = new CinemaController(mock.Object);
            ;

            //Act
            ActionResult result = controller.PinCheck(1);


            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "PinComplete");
        }

        [TestMethod]
        public void PinChecknotpaid()
        {
            //Arrange
            List<Ticket> Tickets = new List<Ticket> { 
            new Ticket { TicketID = 23,
                Order = new Order { OrderID = 1, Status = "Gereserveerd", Totaalprijs = 25.00 }, 
                Seat = new Seat { RowNumber = 4,  SeatNumber = 6}, 
                Show = new Show { Screen = new Screen { ScreenID = 1, Size = 120 }, Movie = new Movie{ Title = "Big Hero 6"}, Is3D = true, Language = new Language { LanguageName = "Nederlands"}}, 
                Tariff = new Tariff{ Name = "Normaal", Price = 8.00}
            }};

            Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();
            mock.Setup(m => m.Orders).Returns(new List<Order> {
            new Order{OrderID = 1, Status = "Gereserveerd", Totaalprijs = 25.00, Tickets = Tickets}});
            CinemaController controller = new CinemaController(mock.Object);
            ;

            //Act
            ActionResult result = controller.PinCheck(1);


            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "PinViewReservation");
        }

        [TestMethod]
        public void PinCheckNoOrders()
        {
            //Arrange
            Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();
            mock.Setup(m => m.Orders).Returns(new List<Order>());
                CinemaController controller = new CinemaController(mock.Object);
            ;

            //Act
            ActionResult result = controller.PinCheck(1);
            

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void IndexTest()
        {
            //Arrange
            Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();
            CinemaController controller = new CinemaController(mock.Object);

            //Act
            ViewResult result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ReserveringOphalenTest()
        {
            //Arrange
            Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();
            CinemaController controller = new CinemaController(mock.Object);

            //Act
            ViewResult result = controller.ReserveringOphalen();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DeleteOrderCheck()
        {
            //Arrange
            List<Ticket> Tickets = new List<Ticket> { 
            new Ticket { TicketID = 23,
                Order = new Order { OrderID = 1, Status = "Gereserveerd", Totaalprijs = 25.00 }, 
                Seat = new Seat { RowNumber = 4,  SeatNumber = 6}, 
                Show = new Show { Screen = new Screen { ScreenID = 1, Size = 120 }, Movie = new Movie{ Title = "Big Hero 6"}, Is3D = true, Language = new Language { LanguageName = "Nederlands"}}, 
                Tariff = new Tariff{ Name = "Normaal", Price = 8.00}
            }};

            Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();
            mock.Setup(m => m.Orders).Returns(new List<Order> {
            new Order{OrderID = 1, Status = "Gereserveerd", Totaalprijs = 25.00, Tickets = Tickets}});
            CinemaController controller = new CinemaController(mock.Object);
            ;

            //Act
            ViewResult result = controller.DeleteOrder(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AskOrderView() {
            // Arrange
            Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();
            mock.Setup(s => s.Shows).Returns(testshows);

            // Act
            CinemaController controller = new CinemaController(mock.Object);
            ViewResult result = controller.Order(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void AskViewIndex() {
            // Arrange
            Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();
            mock.Setup(s => s.Shows).Returns(testshows);

            // Act
            CinemaController controller = new CinemaController(mock.Object);
            ViewResult result = controller.Order(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

    }
}
