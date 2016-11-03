using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IVH7_Cinema.WebUI.HtmlHelpers;
using IVH7_Cinema.WebUI.Models;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.UnitTests
{
    [TestClass][ExcludeFromCodeCoverage]
    public class SubscriberTests
    {
        [TestMethod]
        public void GetSubscribersTest()
        {
            //Arrange
            // create some mock Subscribers to play with
            List<Subscriber> SubscribersMoq = new List<Subscriber>{
                new Subscriber{ Name = "Sub1", Email = "Sub1@subscribe.com" },
                new Subscriber{ Name = "Sub2", Email = "Sub2@subscribe.com" },
                new Subscriber{ Name = "Sub3", Email = "Sub3@subscribe.com" }
            };

            // Mock the Repository using Moq
            Mock<ICinemaRepository> mockCinemaRepository = new Mock<ICinemaRepository>();

            // Return all the subscribers
            mockCinemaRepository.Setup(rep => rep.Subscribers).Returns(SubscribersMoq);

            // Act
            List<Subscriber> Subscribers = mockCinemaRepository.Object.Subscribers.ToList<Subscriber>();

            //assert
            Assert.AreEqual(Subscribers.Count(), 3);
        }

        //[TestMethod]
        //public void DeleteSubscriberTest()
        //{
        //    //Arrange
        //    // create some mock Subscribers to play with
        //    List<Subscriber> SubscribersMoq = new List<Subscriber>{
        //        new Subscriber{ Name = "Sub1", Email = "Sub1@subscribe.com" },
        //        new Subscriber{ Name = "Sub2", Email = "Sub2@subscribe.com" },
        //        new Subscriber{ Name = "Sub3", Email = "Sub3@subscribe.com" }
        //    };

        //    // Mock the Repository using Moq
        //    Mock<ICinemaRepository> mockCinemaRepository = new Mock<ICinemaRepository>();

        //    // Return all the subscribers
        //    mockCinemaRepository.Setup(rep => rep.Subscribers).Returns(SubscribersMoq);

        //    // Act
        //    mockCinemaRepository.Object.DeleteSubscriber(SubscribersMoq[1]);

        //    //assert
        //    Assert.AreEqual(3, SubscribersMoq.Count());
        //}

    }
}
