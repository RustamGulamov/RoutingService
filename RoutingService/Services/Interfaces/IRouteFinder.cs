using RoutingService.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RoutingService.Services.Interfaces
{
    public interface IRouteFinder
    {
        Task<Flight[]> FindRouteAsync(string srcAirport, string destAirport, CancellationToken ct);
    }
}
