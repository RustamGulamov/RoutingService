namespace RoutingService
{
    public class RouteFinderConfig
    {
        public int MaxRouteDepth { get; set; }
        public int MaxDegreeOfParallelism { get; set; }
        public int RetryRequestCount { get; set; }
    }
}
