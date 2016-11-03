using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using IVH7_Cinema.Domain.Abstract;
using IVH7_Cinema.Domain.Entities;
using System.Linq;
using IVH7_Cinema.WebUI.Controllers;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.UnitTests {
    [TestClass][ExcludeFromCodeCoverage]
    public class TariffTest {
        [TestMethod]
        public void Is_Normal_Tariff() {
            // Arrange - create the mock repository
            Show TestShow = new Show {
                Movie = new Movie { Title = "Movie A", Is3DAvailable = false, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 110, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the controller
            Tariff target = new Tariff() { Name = "Normaal", Price = 8.50 };

            //Arrange - set the expected tariff
            Double expected = 8.50;

            // Act - call the calculate action method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Is_120_Longer() {
            // Arrange - create a TestShow
            Show TestShow = new Show {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = false, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 140, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "Normaal", Price = 8.50 };

            //Arrange - set the expected tariff
            Double expected = 9.00;

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Is_65_plus_Korting() {
            // Arrange - create a TestShow
            Show TestShow = new Show {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = false, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 110, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "65+ Reductie", Price = 7.00 };

            //Arrange - set the expected tariff
            Double expected = 7.00;

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Is_Student_Korting() {
            // Arrange - create a TestShow
            Show TestShow = new Show {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = false, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 110, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "Studentenkorting", Price = 7.00 };

            //Arrange - set the expected tariff
            Double expected = 7.00;

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Is_3D_Movie() {
            // Arrange - create a TestShow
            Show TestShow = new Show {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = false, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 110, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = true,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "3D Film", Price = 1.50 };

            //Arrange - set the expected tariff
            Double expected = 1.50;

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Is_65_plus_Korting_Langer_120() {
            // Arrange - create a TestShow
            Show TestShow = new Show {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = false, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 130, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "65+ Reductie", Price = 7.50 };

            //Arrange - set the expected tariff
            Double expected = 8.00;

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Is_Kinder_Korting() {
            // Arrange - create a TestShow
            Show TestShow = new Show {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = false, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 110, Genres = new List<Genre> { new Genre { Name = "Kinderfilm" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };
            DateTime date = DateTime.Now;

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "Kinderkorting", Price = 7.00 };

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            Assert.AreEqual(7.00, actual);

        }

        [TestMethod]
        public void Is_65_Plus_Vakantie() {
            // Arrange - create a TestShow
            Show TestShow = new Show {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = false, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 110, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "65+ Reductie", Price = 7.00};

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            if(DateTime.Now.DayOfWeek.ToString().Equals("Monday")||
               DateTime.Now.DayOfWeek.ToString().Equals("Tuesday") ||
               DateTime.Now.DayOfWeek.ToString().Equals("Wednesday") ||
               DateTime.Now.DayOfWeek.ToString().Equals("Thursday")) {
                   Assert.AreEqual(7.00, actual);
            } else {
                Assert.AreEqual(8.50, actual);
            }
            
        }

        [TestMethod]
        public void Is_Student_No_Discount() {
            // Arrange - create a TestShow
            Show TestShow = new Show {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = false, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 110, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "Studentenkorting", Price = 7.50};

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            if (DateTime.Now.DayOfWeek.ToString().Equals("Monday") ||
                DateTime.Now.DayOfWeek.ToString().Equals("Tuesday") ||
                DateTime.Now.DayOfWeek.ToString().Equals("Wednesday") ||
                DateTime.Now.DayOfWeek.ToString().Equals("Thursday")) {
                Assert.AreEqual(7.50, actual);
            } else {
                Assert.AreEqual(9.00, actual);
            }
        }

        [TestMethod]
        public void Is_Student_Korting_Langer_120() {
            // Arrange - create a TestShow
            Show TestShow = new Show {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = false, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 130, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "Studentenkorting", Price = 7.50};

            //Arrange - set the expected tariff
            Double expected = 8.00;

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Is_3DBril()
        {
            // Arrange - create a TestShow
            Show TestShow = new Show
            {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = true, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 110, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "3D Bril", Price = 1.50 };

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            Assert.AreEqual(actual, 1.50);

        }

        [TestMethod]
        public void Is_PopcornArrangement()
        {
            // Arrange - create a TestShow
            Show TestShow = new Show
            {
                Movie = new Movie { Title = "MovieB", Is3DAvailable = true, Languages = new List<Language> { new Language { LanguageName = "Nederlands" } }, Duration = 110, Genres = new List<Genre> { new Genre { Name = "GenreA" } }, Ratings = new List<Rating> { new Rating { Name = "Seksueel" }, new Rating { Name = "Geweld" } } },
                Is3D = false,
                Language = new Language { LanguageName = "Nederlands" }
            };

            // Arrange - create the Tariff
            Tariff target = new Tariff() { Name = "Popcorn Arrangement", Price = 1.50 };

            // Act - call the calculate method
            Double actual = target.calculatePrice(TestShow);

            // Assert
            Assert.AreEqual(actual, 6.50);

        }
    }
}

