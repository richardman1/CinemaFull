using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.UnitTests.WebUI {

    [TestClass][ExcludeFromCodeCoverage]
    public class HTMLHelperTest {
        [TestMethod]
        public void GetImplementedCultureDutch() {
            //Arrange
            String expected = "nl-NL";

            //Act
            String result = IVH7_Cinema.WebUI.Website.HtmlHelpers.CultureHelpers.GetImplementedCulture("nl-NL");

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetImplementedCultureEnglish() {
            // Arrange
            string expected = "en-GB";

            // Act
            String result = IVH7_Cinema.WebUI.Website.HtmlHelpers.CultureHelpers.GetImplementedCulture("en-GB");

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetImplementedCultureNotKnown() {
            // Arrange
            string expected = "nl-NL";

            // Act
            String result = IVH7_Cinema.WebUI.Website.HtmlHelpers.CultureHelpers.GetImplementedCulture("en-PL");

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DefaultCulture() {
            // Arrange
            String expected = "nl-NL";

            // Act
            String result = IVH7_Cinema.WebUI.Website.HtmlHelpers.CultureHelpers.GetDefaultCulture();

            // Assert
            Assert.AreEqual(expected, result);
        }

    }
}
