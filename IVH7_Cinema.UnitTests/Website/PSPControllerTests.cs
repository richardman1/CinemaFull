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
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PSPControllerTests
    {
        //Mock repository
        Mock<ICinemaRepository> RepMock = new Mock<ICinemaRepository>();
        Mock<IMailer> MailMock = new Mock<IMailer>();
        Mock<IPrinter> PrinterMock = new Mock<IPrinter>();


        [TestMethod]
        public void RemoveOrderOnExit()
        {
            //Arrange
            List<Ticket> Tickets = new List<Ticket> { 
            new Ticket { TicketID = 23,
                Order = new Order { OrderID = 1, Status = "Gereserveerd", Totaalprijs = 25.00 }, 
                Seat = new Seat { RowNumber = 4,  SeatNumber = 6}, 
                Show = new Show { Screen = new Screen { ScreenID = 1, Size = 120 }, Movie = new Movie{ Title = "Big Hero 6"}, Is3D = true, Language = new Language { LanguageName = "Nederlands"}}, 
                Tariff = new Tariff{ Name = "Normaal", Price = 8.00}
            }};

            RepMock.Setup(x => x.Orders).Returns(new List<Order>{
            new Order{OrderID = 1, Status = "Gereserveerd", Totaalprijs = 25.00, Tickets = Tickets}});

            PSPController controller = new PSPController(RepMock.Object, PrinterMock.Object, MailMock.Object);

            ActionResult result = controller.RemoveOrderOnExit(1);


            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");
        }
    }
}
