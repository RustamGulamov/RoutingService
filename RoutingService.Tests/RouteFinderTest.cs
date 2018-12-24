using NUnit.Framework;
using RoutingService.Models;
using RoutingService.Services;
using RoutingService.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutingService.Tests
{
    [TestFixture]
    public class RouteFinderTest
    {
        private readonly RouteFinder routeFinder = Factory.CreateMockedRouteFinder(8);

        [Test]
        public void TestFindRouteForIncorrectAirports()
        {
            Flight[] route = routeFinder.FindRouteAsync("INCORRECT_AIRPORT_1", "INCORRECT_AIRPORT_2", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(0, route.Length);
        }

        [Test]
        public void TestFindRouteWhenAirlineIsInactive()
        {
            Flight[] route = routeFinder.FindRouteAsync("SVO", "VOZ", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(0, route.Length);
        }

        [Test]
        public void TestFindOneStepRoute()
        {
            Flight[] route = routeFinder.FindRouteAsync("SVO", "KBP", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(1, route.Length);
            Assert.AreEqual("SVO", route[0].SrcAirport);
            Assert.AreEqual("KBP", route[0].DestAirport);
            Assert.AreEqual("UJ", route[0].Airline);
        }

       

        [Test]
        public void TestFindUnavailableRouteInCycle()
        {
            Flight[] route = routeFinder.FindRouteAsync("CYCLE_1", "VOZ", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(0, route.Length);
        }

        [Test]
        public void TestFindRouteWhenMaxDepthReached()
        {
            RouteFinder limitedRouteFinder = Factory.CreateMockedRouteFinder(2);
            Flight[] route = limitedRouteFinder.FindRouteAsync("LINE_1", "LINE_4", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(0, route.Length);

            route = routeFinder.FindRouteAsync("LINE_1", "LINE_4", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreNotEqual(0, route.Length);
        }

        [Test]
        public void TestFindRouteFromSVOToDWC()
        {
            Flight[] route = routeFinder.FindRouteAsync("SVO", "DWC", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(2, route.Length);
            Assert.AreEqual("SVO", route[0].SrcAirport);
            Assert.AreEqual("ODS", route[0].DestAirport);
            Assert.AreEqual("ODS", route[1].SrcAirport);
            Assert.AreEqual("DWC", route[1].DestAirport);
        }

        [Test]
        public async Task TestFindRouteWhenServiceThrowsException()
        {
            bool isException = false;
            try
            {
                Flight[] route = await routeFinder.FindRouteAsync("HAS_ERROR_CHILD", "VOZ", CancellationToken.None);
            }
            catch (Exception e)
            {
                isException = true;
            }

            Assert.IsTrue(isException);
        }

    }
}
