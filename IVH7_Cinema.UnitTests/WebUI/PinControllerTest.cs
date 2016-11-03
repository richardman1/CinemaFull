using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IVH7_Cinema.WebUI.Controllers;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.Domain.Abstract;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System.Diagnostics.CodeAnalysis;
using Moq;
using System.Web.Routing;
using System.Collections.Specialized;
using IVH7_Cinema.WebUI.Website.Models;
using System.Linq;
using IVH7_Cinema.WebUI.Website.Controllers;

namespace IVH7_Cinema.UnitTests {
    [TestClass][ExcludeFromCodeCoverage]
    public class PinControllerTest {
        Mock<IPrinter> printer = new Mock<IPrinter>();
        Mock<ICinemaRepository> repo = new Mock<ICinemaRepository>();
        Mock<IMailer> mailer = new Mock<IMailer>();

        [TestMethod]
        public void PinControllerPinComplete() {
            printer.Setup(x => x.PrintA4Ticket(null, 0, 0));

            repo.Setup(x => x.Orders).Returns(new List<Order> {
                new Order() { OrderID = 1, Totaalprijs = 19.0, Tickets = new List<Ticket>() { 
                    new Ticket() { 
                        Seat = new Seat() { RowNumber = 3, SeatNumber = 6}, 
                        Show = new Show() { DateTime = DateTime.Today, Is3D = true }, 
                        Order = new Order() { OrderID = 1, Totaalprijs = 19}, OrderID = 1
                        }
                    }
                }
            });

            PinController controller = new PinController(repo.Object, printer.Object);
            ActionResult result = controller.PinComplete(1, 0, 0);

            Assert.IsInstanceOfType(result, typeof(ActionResult));

        }

        [TestMethod]
        public void LuhnChecktrue()
        {
            //Arrange
            string cardNumber = "1234567812345670";

            //Act
            bool result = IVH7_Cinema.WebUI.Website.Models.Luhn.LuhnCheck(cardNumber);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LuhnCheckFalse()
        {
            //Arrange
            string cardNumber = "1234567812345671";

            //Act
            bool result = IVH7_Cinema.WebUI.Website.Models.Luhn.LuhnCheck(cardNumber);
            
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PinControllerPinViewReservation()
        {
            //Arrange
            List<Ticket> Tickets = new List<Ticket> { 
            new Ticket { TicketID = 23,
                Order = new Order { OrderID = 1, Status = "Gereserveerd", Totaalprijs = 25.00 }, 
                Seat = new Seat { RowNumber = 4,  SeatNumber = 6}, 
                Show = new Show { Screen = new Screen { ScreenID = 1, Size = 120 }, Movie = new Movie{ Title = "Big Hero 6"}, Is3D = true, Language = new Language { LanguageName = "Nederlands"}}, 
                Tariff = new Tariff{ Name = "Normaal", Price = 8.00}
            }};

            Mock<IPrinter> printer = new Mock<IPrinter>();
            Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();
            Order o1 = new Order{OrderID = 1, Status = "Gereserveerd", Totaalprijs = 25.00, Tickets = Tickets};
            PinController controller = new PinController(mock.Object, printer.Object);
            ;

            //Act
            ActionResult result = controller.PinViewReservation(o1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
