using RoutingService.Models;
using RoutingService.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoutingService.Services
{
    public class FlightsApiClient : IFlightsApiClient
    {
        private readonly HttpClient httpClient;

        public FlightsApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Airline> GetAirlineAsync(string alias)
        {
            var airlines = await GetJsonAsync<Airline[]>($"/api/Airline/{alias}");
            return airlines.Length > 0 ? airlines[0] : null;
        }

        public Task<Flight[]> GetOutgoingFlightsAsync(string airportCode)
        {
            return GetJsonAsync<Flight[]>($"/api/Route/outgoing?airport={airportCode}");
        }

        public Task<Airport[]> SearchAirports(string pattern)
        {
            return GetJsonAsync<Airport[]>($"/api/Airport/search?pattern={pattern}");
        }

        private async Task<TResult> GetJsonAsync<TResult>(string uri)
        {
            try
            {
                HttpResponseMessage resp = await httpClient.GetAsync(uri);
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadAsAsync<TResult>();
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException($"Flights API error: {uri}", e);
            }
        }
    }
}
