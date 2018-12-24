using RoutingService.Models;
using System.Threading.Tasks;

namespace RoutingService.Services.Interfaces
{
    public interface IFlightsApiClient
    {
        Task<Airline> GetAirlineAsync(string alias);
        Task<Flight[]> GetOutgoingFlightsAsync(string airportCode);
        Task<Airport[]> SearchAirports(string pattern);
    }
}
