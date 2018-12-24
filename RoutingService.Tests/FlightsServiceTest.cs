using NUnit.Framework;
using RoutingService.Models;
using RoutingService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using RoutingService.Tests.Mocks;

namespace RoutingService.Tests
{
    [TestFixture]
    public class FlightsServiceTest
    {
        private readonly FlightsService flightsService = Factory.CreateMockedFlightsService();

        [Test]
        public void TestGetActiveOutgoingFlightsForIncorrectAirport()
        {
            var flights = flightsService.GetActiveOutgoingFlightsAsync("INCORRECT_CODE").Result;
            Assert.IsNotNull(flights);
            Assert.AreEqual(0, flights.Count);
        }

        [Test]
        public void TestGetActiveOutgoingFlights()
        {
            var flights = flightsService.GetActiveOutgoingFlightsAsync("SVO").Result;
            Assert.IsNotNull(flights);
            Assert.AreEqual(2, flights.Count);
            Assert.AreEqual("UJ", flights[0].Airline);
            Assert.AreEqual("UJ", flights[1].Airline);
        }

        [Test]
        public void TestValidateAirportCode()
        {
            var validationResult = flightsService.ValidateAirportCodeAsync("A").Result;
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(ValidationErrorType.BadFormat, validationResult.ErrorType);

            validationResult = flightsService.ValidateAirportCodeAsync("AAAAAAAA").Result;
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(ValidationErrorType.BadFormat, validationResult.ErrorType);

            validationResult = flightsService.ValidateAirportCodeAsync("EMPT").Result;
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(ValidationErrorType.NotFound, validationResult.ErrorType);

            validationResult = flightsService.ValidateAirportCodeAsync("VOZ").Result;
            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
