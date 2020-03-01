using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTransportData.Core.Enums;
using OpenTransportData.Service.Train;

namespace OpenTransportData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrainService _trainService;
        public TrainController(ITrainService trainService)
        {
            _trainService = trainService;
        }

        /// <summary>
        /// Returns all UK station codes and names.
        /// </summary>
        /// <returns></returns>
        [HttpGet("stations")]
        public async Task<IActionResult> GetStations()
        {
            var stations = await _trainService.GetAllStationsAsync();

            return Ok(stations);
        }

        /// <summary>
        /// Returns all arrivals to the requested station, optionally filtered by the timeWindow parameter.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window in minutes to fetch.</param>
        /// <returns></returns>
        [HttpGet("{crs}/arrivals")]
        public async Task<IActionResult> GetStationArrivals(string crs, int timeWindow = 120)
        {
            var arrivals = await _trainService.GetStationArrivalsAsync(crs, timeWindow);
            
            return Ok(arrivals);
        }

        /// <summary>
        /// Returns all depatures to the requested station, optionally filtered by the timeWindow parameter.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window in minutes to fetch.</param>
        /// <returns></returns>
        [HttpGet("{crs}/departures")]
        public async Task<IActionResult> GetStationDepartures(string crs, int timeWindow = 120)
        {
            var departures = await _trainService.GetStationDeparturesAsync(crs, timeWindow);

            return Ok(departures);
        }

        /// <summary>
        /// Returns all arrivals to the requested station, along with each entries' calling points, optionally filtered by the timeWindow parameter.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window in minutes to fetch.</param>
        /// <returns></returns>
        [HttpGet("{crs}/arrivals/detail")]
        public async Task<IActionResult> GetDetailedStationArrivals(string crs, int timeWindow = 120)
        {
            var arrivals = await _trainService.GetDetailedStationArrivalsAsync(crs, timeWindow);

            return Ok(arrivals);
        }

        /// <summary>
        /// Returns all departures to the requested station, along with each entries' calling points, optionally filtered by the timeWindow parameter.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window in minutes to fetch.</param>
        /// <returns></returns>
        [HttpGet("{crs}/departures/detail")]
        public async Task<IActionResult> GetDetailedStationDepartures(string crs, int timeWindow = 120)
        {
            var departures = await _trainService.GetDetailedStationDeparturesAsync(crs, timeWindow);

            return Ok(departures);
        }

        /// <summary>
        /// Returns details on the specified service, returning travel information and calling point information on the service.
        /// </summary>
        /// <param name="serviceID">ID of the service to fetch</param>
        /// <returns></returns>
        [HttpGet("service/{serviceID}")]
        public async Task<IActionResult> GetServiceDetails(string serviceID)
        {
            var details = await _trainService.GetServiceDetailsAsync(serviceID);

            return Ok(details);
        }
    }
}