using RoutingService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoutingService.Tests.Mocks
{
    public class AirlinesFixtures
    {
        private Dictionary<string, Airline> data;

        public AirlinesFixtures()
        {
            data = new Dictionary<string, Airline>
            {
                {"UJ", new Airline() {Active = true, Alias = "UJ", Name = "AlMasria Universal Airlines"}},
                {"FZ", new Airline() {Active = true, Alias = "FZ", Name = "Fly Dubai"}},
                {
                    "INACTIVE_AIRLINE",
                    new Airline() {Active = false, Alias = "INACTIVE_AIRLINE", Name = "Inactive airline"}
                }
            };
        }

        public Airline GetAirline(string alias)
        {
            return data.TryGetValue(alias, out var airline) ? airline : null;
        }
    }
}
