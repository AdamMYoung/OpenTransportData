using OpenTransportData.Core.Enums;
using OpenTransportData.Service.Train.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenTransportData.Service.Train
{
    public interface ITrainService
    {
        Task<IEnumerable<Station>> GetAllStationsAsync();

        Task<IEnumerable<TimetableEntry>> GetStationTimetableAsync(string crs, TimetableTypes timetableType);     
    }
}
