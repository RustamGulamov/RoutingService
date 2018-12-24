using RoutingService.Models;
using RoutingService.Services.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RoutingService.Services
{
    public class AirlinesCache : IAirlinesCache
    {
        private readonly ConcurrentDictionary<string, Airline> airlinesDictionary = new ConcurrentDictionary<string, Airline>();

        public Airline Get(string alias) => airlinesDictionary.GetValueOrDefault(alias);

        public void Set(Airline airline) => airlinesDictionary.TryAdd(airline.Alias, airline);
    }
}
