using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using IVH7_Cinema.WebUI.Controllers;
using IVH7_Cinema.WebUI.Models;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.Domain.Abstract;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using System.Globalization;

namespace IVH7_Cinema.UnitTests.WebUI
{
    [TestClass][ExcludeFromCodeCoverage]
    public class CinemaControllerWebUITest
    {
        //Mock Repository
        Mock<ICinemaRepository> RepMock = new Mock<ICinemaRepository>();

        // Some test shows
        List<Show> Shows = new List<Show>{
            new Show { ShowID = 1, Movie =  new Movie { MovieID = 1, Title = "Gooise Vrouwen", Duration = 120, Genres = new List<Genre> { new Genre { Name = "Women" } }, Is3DAvailable = false, Languages = new Language[] { new Language() { LanguageID = 1, LanguageName = "Engels"}}}, MovieID = 1, CinemaID = 1, ScreenID = 1, DateTime = DateTime.Today,Language = new Language {LanguageID = 1, LanguageName = "Nederlands"}, LanguageID = 1 },
            new Show { ShowID = 2, Movie = new Movie { MovieID = 2, Title = "50 shades of grey", Duration = 130, Genres = new List<Genre> { new Genre { Name = "Erotic" } }, Is3DAvailable = false }, MovieID = 2, CinemaID = 1, ScreenID = 1, DateTime = DateTime.Today,Language = new Language {LanguageID = 1, LanguageName = "Nederlands"}, LanguageID = 1 }
        };

        [TestMethod]
        public void FilmIndexTest()
        {
            // Arrange - create the controller
            CinemaController target = new CinemaController(RepMock.Object);
            

            // Act - call the action method
            ViewResult result = target.FilmIndex();

            // Assert - check the result
            Assert.AreEqual("FilmIndex", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ShowsListViewModel));
        }

        [TestMethod]
        public void CinemaControllerOrderTest()
        {
            // Arrange - create the controller
            CinemaController target = new CinemaController(RepMock.Object);
            
            Int64 showId = 1;

            RepMock.Setup(x => x.Shows).Returns(Shows);

            // Act - call the action method
            ViewResult result = target.Order(showId);
            

            // Assert - check the result
            Assert.AreEqual("Order", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Show));
        }



    }
}
