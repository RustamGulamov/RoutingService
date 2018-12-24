namespace RoutingService.Models
{
    public class Flight
    {
        public Flight(string airline, string srcAirport, string destAirport)
        {
            Airline = airline;
            SrcAirport = srcAirport;
            DestAirport = destAirport;
        }

        public string Airline { get; }
        public string SrcAirport { get; }
        public string DestAirport { get; }
    }
}
