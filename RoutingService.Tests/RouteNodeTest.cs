using System.Linq;
using NUnit.Framework;
using RoutingService.Models;
using RoutingService.Services;

namespace RoutingService.Tests
{
    [TestFixture]
    public class RouteNodeTest
    {
        [Test]
        public void TestGetOneFlightRoute()
        {
            var flight = new Flight("airline", "src", "dest");
            var first = new RouteNode("src", null, null);
            var second = new RouteNode("dest", new Flight("airline", "src", "dest"), first);

            var route = second.GetFullRoute();
            Assert.IsNotNull(route);
            Assert.AreEqual(1, route.Length);
            Assert.AreEqual("airline", route[0].Airline);
            Assert.AreEqual("src", route[0].SrcAirport);
            Assert.AreEqual("dest", route[0].DestAirport);
        }

        [Test]
        public void TestGetLongRout()
        {
            var flights = new Flight[3] {
                new Flight("airline_1", "airport_1", "airport_2"),
                new Flight("airline_2", "airport_2", "airport_3"),
                new Flight("airline_3", "airport_3", "airport_4"),
            };
            var node = new RouteNode("airport_1", null, null);
            node = flights.Aggregate(node, (current, flight) => new RouteNode(flight.DestAirport, flight, current));

            var route = node.GetFullRoute();
            Assert.IsNotNull(route);
            Assert.AreEqual(3, route.Length);
            for (var i = 0; i < route.Length; i++)
            {
                Assert.AreEqual(flights[i].Airline, route[i].Airline);
                Assert.AreEqual(flights[i].SrcAirport, route[i].SrcAirport);
                Assert.AreEqual(flights[i].DestAirport, route[i].DestAirport);
            }
        }
    }
}
