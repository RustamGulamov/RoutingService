using RoutingService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoutingService.Services.Interfaces
{
    public interface IFlightsService
    {
        Task<List<Flight>> GetActiveOutgoingFlightsAsync(string airportCode);
        Task<ValidationResult> ValidateAirportCodeAsync(string airportCode);
    }
}
