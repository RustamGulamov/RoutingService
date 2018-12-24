using RoutingService.Models;
using System;

namespace RoutingService.Services
{
    public class RouteNode
    {
        private readonly Flight flight;
        private readonly RouteNode prevNode;

        public RouteNode(string airport, Flight flight, RouteNode prevNode)
        {
            this.prevNode = prevNode;
            this.flight = flight;
            Airport = airport;
            Depth = prevNode?.Depth + 1 ?? 0;
        }

        public string Airport { get; }
        public int Depth { get; }

        public Flight[] GetFullRoute()
        {
            if (Depth == 0)
            {
                return Array.Empty<Flight>();
            }

            var result = new Flight[Depth];
            var node = this;
            for (var i = Depth; i > 0; i--)
            {
                result[i - 1] = node.flight;
                node = node.prevNode;
            }
            return result;
        }
    }
}
