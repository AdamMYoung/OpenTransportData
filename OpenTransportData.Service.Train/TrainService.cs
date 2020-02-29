using Microsoft.Extensions.Options;
using NationalRailDarwin;
using OpenTransportData.Core.Enums;
using OpenTransportData.Service.Train.Models;
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

        public TrainService(IStationLoader stationLoader,
            IOptions<TrainServiceOptions> options)
        {
            _stationLoader = stationLoader;
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
        /// <returns></returns>
        public async Task<IEnumerable<TimetableEntry>> GetStationArrivalsAsync(string crs)
        {
            var arrivalBoard = await _ldbService.GetArrivalBoardAsync(new GetArrivalBoardRequest(_accessToken, 30, crs.ToUpper(), "", FilterType.to, 0, 120));
            var boardResult = arrivalBoard.GetStationBoardResult;

            return boardResult.trainServices.Select(t => new TimetableEntry()
            {
                Origin = t.origin[0].locationName,
                Destination = t.destination[0].locationName,
                Platform = t.platform,
                PlannedTime = DateTime.Parse(t.sta),
                EstimatedTime = GetEstimatedTime(t.sta, t.eta),
                Length = t.length,
                Operator = t.@operator,
                DelayReason = t.delayReason,
                CancellationReason = t.cancelReason,
                Status = GetServiceStatus(t, TimetableTypes.Arrival)
            });
        }

        /// <summary>
        /// Gets the departure timetable for the provided station.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TimetableEntry>> GetStationDeparturesAsync(string crs)
        {
            var arrivalBoard = await _ldbService.GetDepartureBoardAsync(new GetDepartureBoardRequest(_accessToken, 30, crs.ToUpper(), "", FilterType.from, 0, 120));
            var boardResult = arrivalBoard.GetStationBoardResult;

            return boardResult.trainServices.Select(t => new TimetableEntry()
            {
                Origin = t.origin[0].locationName,
                Destination = t.destination[0].locationName,
                Platform = t.platform,
                PlannedTime = DateTime.Parse(t.std),
                EstimatedTime = GetEstimatedTime(t.std, t.etd),
                Length = t.length,
                Operator = t.@operator,
                DelayReason = t.delayReason,
                CancellationReason = t.cancelReason,
                Status = GetServiceStatus(t, TimetableTypes.Departure)
            });
           
        }

        /// <summary>
        /// Returns the status of the service.
        /// </summary>
        /// <param name="service">Service to get the status from.</param>
        /// <returns></returns>
        private TravelStatus GetServiceStatus(ServiceItem1 service, TimetableTypes boardType)
        {
            if (service.isCancelled) return TravelStatus.Cancelled;

            switch (boardType)
            {
                case TimetableTypes.Arrival:
                    if (service.eta.ToLower() == "on time") return TravelStatus.OnTime;
                    break;
                case TimetableTypes.Departure:
                    if (service.etd.ToLower() == "on time") return TravelStatus.OnTime;
                    break;
                default:
                    throw new NotImplementedException($"Board type not implemented: {boardType}");
            }

            return TravelStatus.Delayed;
        }

        /// <summary>
        /// Returns the estimated time from the provided planned and estimated strings.
        /// </summary>
        /// <param name="planned">String of the planned arrival/departure.</param>
        /// <param name="estimated">String of the estimated arrival/departure.</param>
        /// <returns></returns>
        private DateTime GetEstimatedTime(string planned, string estimated)
        {
            if (estimated.ToLower() == "on time" || estimated.ToLower() == "cancelled")
                return DateTime.Parse(planned);
            else
                return DateTime.Parse(estimated);
        }
    }
}
