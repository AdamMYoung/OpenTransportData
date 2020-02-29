using OpenTransportData.Core.Enums;
using OpenTransportData.Service.Train.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenTransportData.Service.Train
{
    public interface ITrainService
    {
        /// <summary>
        /// Returns all possible stations to query.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Station>> GetAllStationsAsync();

        /// <summary>
        /// Gets the arrival timetable for the provided station.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <returns></returns>
        Task<IEnumerable<TimetableEntry>> GetStationArrivalsAsync(string crs);

        /// <summary>
        /// Gets the departure timetable for the provided station.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <returns></returns>
        Task<IEnumerable<TimetableEntry>> GetStationDeparturesAsync(string crs);
    }
}
