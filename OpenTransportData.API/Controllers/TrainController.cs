using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OpenTransportData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        [HttpGet("{crs}/arrivals")]
        public IActionResult GetArrivals(string crs)
        {
            return Ok("Arrivals");
        }

        [HttpGet("{crs}/departures")]
        public IActionResult GetDepartures(string crs)
        {
            return Ok("Departures");
        }

        [HttpGet("stations")]
        public IActionResult GetStations()
        {
            return Ok();
        }
    }
}