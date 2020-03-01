using Microsoft.Extensions.Options;
using NationalRailDarwin;
using OpenTransportData.Core.Enums;
using OpenTransportData.Service.Train.Models;
using OpenTransportData.Service.Train.Parser;
using OpenTransportData.Utility.StationLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTransportData.Service.Train
{
    public class TrainService : ITrainService
    {
        private readonly LDBServiceSoap _ldbService = new LDBServiceSoapClient(LDBServiceSoapClient.EndpointConfiguration.LDBServiceSoap);
        private readonly AccessToken _accessToken;
        private readonly IStationLoader _stationLoader;
        private readonly IDarwinParser _darwinParser;

        public TrainService(IStationLoader stationLoader,
            IDarwinParser darwinParser,
            IOptions<TrainServiceOptions> options)
        {
            _stationLoader = stationLoader;
            _darwinParser = darwinParser;
            _accessToken = new AccessToken() { TokenValue = options.Value.AccessToken };
        }

        /// <summary>
        /// Returns all possible stations to query.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Station>> GetAllStationsAsync()
        {
            var stations = await Task.Run(() => _stationLoader.GetAll());

            return stations.Select(s => new Station()
            {
                Name = s.Name,
                CRS = s.CRS
            });
        }

        /// <summary>
        /// Gets the arrival timetable for the provided station.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window to fetch.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TimetableEntry>> GetStationArrivalsAsync(string crs, int timeWindow)
        {
            var arrivalBoard = await _ldbService.GetArrivalBoardAsync(new GetArrivalBoardRequest(_accessToken, 30, crs.ToUpper(), "", FilterType.to, 0, timeWindow));
            var boardResult = arrivalBoard.GetStationBoardResult;

            return _darwinParser.Parse(boardResult.trainServices, TimetableTypes.Arrival);
        }

        /// <summary>
        /// Gets the departure timetable for the provided station.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window to fetch.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TimetableEntry>> GetStationDeparturesAsync(string crs, int timeWindow)
        {
            var departureBoard = await _ldbService.GetDepartureBoardAsync(new GetDepartureBoardRequest(_accessToken, 30, crs.ToUpper(), "", FilterType.from, 0, timeWindow));
            var boardResult = departureBoard.GetStationBoardResult;

            return _darwinParser.Parse(boardResult.trainServices, TimetableTypes.Departure);
        }

        /// <summary>
        /// Gets the arrival timetable for the provided station, along with all calling points of the service.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window to fetch.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TimetableEntryDetail>> GetDetailedStationArrivalsAsync(string crs, int timeWindow)
        {
            var arrivalBoard = await _ldbService.GetDepBoardWithDetailsAsync(new GetDepBoardWithDetailsRequest(_accessToken, 30, crs.ToUpper(), "", FilterType.to, 0, timeWindow));
            var board = arrivalBoard.GetStationBoardResult;

            return _darwinParser.Parse(board.trainServices, TimetableTypes.Arrival);
        }

        /// <summary>
        /// Gets the departure timetable for the provided station, along with all calling points of the service.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window to fetch.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TimetableEntryDetail>> GetDetailedStationDeparturesAsync(string crs, int timeWindow)
        {
            var departureBoard = await _ldbService.GetDepBoardWithDetailsAsync(new GetDepBoardWithDetailsRequest(_accessToken, 30, crs.ToUpper(), "", FilterType.from, 0, timeWindow));
            var board = departureBoard.GetStationBoardResult;

            return _darwinParser.Parse(board.trainServices, TimetableTypes.Departure);
        }

        /// <summary>
        /// Gets service details for the provided service.
        /// </summary>
        /// <param name="serviceID">ID of the service.</param>
        /// <returns></returns>
        public async Task<Models.ServiceDetails> GetServiceDetailsAsync(string serviceID)
        {
            var details = await _ldbService.GetServiceDetailsAsync(new GetServiceDetailsRequest(_accessToken, serviceID));
            var result = details.GetServiceDetailsResult;

            return _darwinParser.Parse(result);
        }
    }
}
