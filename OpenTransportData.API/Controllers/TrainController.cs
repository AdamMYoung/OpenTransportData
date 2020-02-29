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

        [HttpGet("{crs}/arrivals")]
        public async Task<IActionResult> GetStationArrivals(string crs)
        {
            var arrivals = await _trainService.GetStationTimetableAsync(crs, TimetableTypes.Arrival);
            
            return Ok(arrivals);
        }

        [HttpGet("{crs}/departures")]
        public async Task<IActionResult> GetStationDepartures(string crs)
        {
            var departures = await _trainService.GetStationTimetableAsync(crs, TimetableTypes.Departure);

            return Ok(departures);
        }

        [HttpGet("stations")]
        public async Task<IActionResult> GetStations()
        {
            var stations = await _trainService.GetAllStationsAsync();
            
            return Ok(stations);
        }
    }
}