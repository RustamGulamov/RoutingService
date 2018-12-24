using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoutingService.Models;
using RoutingService.Services.Interfaces;

namespace RoutingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteFinder routeFinder;
        private readonly IFlightsService flightsService;

        public RouteController(IRouteFinder routeFinder, IFlightsService flightsService)
        {
            this.routeFinder = routeFinder ?? throw new ArgumentNullException(nameof(routeFinder));
            this.flightsService = flightsService ?? throw new ArgumentNullException(nameof(flightsService)); ;
        }

        [HttpGet("search")]
        public async Task<ActionResult<Flight[]>> Search(string srcAirport, string destAirport)
        {
            if (srcAirport == destAirport)
            {
                return BadRequest(new { Message = "Source and destination airports can not be same" });
            }

            var airportValidationResult = await flightsService.ValidateAirportCodeAsync(srcAirport);
            if (!airportValidationResult.IsValid)
            {
                return GetInvalidAirportErrorResponse(airportValidationResult);
            }

            airportValidationResult = await flightsService.ValidateAirportCodeAsync(destAirport);
            if (!airportValidationResult.IsValid)
            {
                return GetInvalidAirportErrorResponse(airportValidationResult);
            }

            var route = await routeFinder.FindRouteAsync(srcAirport, destAirport, CancellationToken.None);
            return Ok(route);
        }

        private ActionResult GetInvalidAirportErrorResponse(ValidationResult validationResult)
        {
            if (validationResult.ErrorType == ValidationErrorType.NotFound)
            {
                return NotFound(new { Message = validationResult.ErrorMessage });
            }
            return BadRequest(new { Message = validationResult.ErrorMessage });
        }
    }
}