using Microsoft.Extensions.Options;
using OpenTransportData.Core.Enums;
using OpenTransportData.Service.Train.Dtos;
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
        private readonly IStationLoader _stationLoader;
        private readonly IOptions<TrainServiceOptions> _options;
        
        public TrainService(IStationLoader stationLoader, 
            IOptions<TrainServiceOptions> options)
        {
            _stationLoader = stationLoader;
            _options = options;
        }

        public async Task<IEnumerable<Station>> GetAllStationsAsync()
        {
            var stations = await Task.Run(() => _stationLoader.GetAll());

            return stations.Select(s => new Station()
            {
                Name = s.Name,
                CRS = s.CRS
            });
        }

        public Task<IEnumerable<TimetableEntry>> GetStationTimetableAsync(string crs, TimetableTypes timetableType)
        {
            throw new NotImplementedException();
        }
    }
}
