using RoutingService.Models;

namespace RoutingService.Services.Interfaces
{
    public interface IAirlinesCache
    {
        Airline Get(string alias);
        void Set(Airline airline);
    }
}
