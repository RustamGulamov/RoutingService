using RoutingService.Services;
using RoutingService.Services.Interfaces;

namespace RoutingService.Tests.Mocks
{
    public static class Factory
    {
        public static FlightsService CreateMockedFlightsService()
        {
            IFlightsApiClient flightsApiClient = new FlightsApiClientMock();
            IAirlinesCache airlinesCache = new AirlinesCache();
            return new FlightsService(flightsApiClient, airlinesCache);
        }

        public static RouteFinder CreateMockedRouteFinder(int maxRouteDepth)
        {
            return new RouteFinder(CreateMockedFlightsService(), new RouteFinderConfig() { MaxRouteDepth = maxRouteDepth, MaxDegreeOfParallelism = 4 });
        }
    }
}
