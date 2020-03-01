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
        /// <param name="timeWindow">Time window to fetch.</param>
        /// <returns></returns>
        Task<IEnumerable<TimetableEntry>> GetStationArrivalsAsync(string crs, int timeWindow);

        /// <summary>
        /// Gets the departure timetable for the provided station.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window to fetch.</param>
        /// <returns></returns>
        Task<IEnumerable<TimetableEntry>> GetStationDeparturesAsync(string crs, int timeWindow);

        /// <summary>
        /// Gets the arrival timetable for the provided station, along with all calling points of the service.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window to fetch.</param>
        /// <returns></returns>
        Task<IEnumerable<TimetableEntryDetail>> GetDetailedStationArrivalsAsync(string crs, int timeWindow);

        /// <summary>
        /// Gets the departure timetable for the provided station, along with all calling points of the service.
        /// </summary>
        /// <param name="crs">CRS of the station.</param>
        /// <param name="timeWindow">Time window to fetch.</param>
        /// <returns></returns>
        Task<IEnumerable<TimetableEntryDetail>> GetDetailedStationDeparturesAsync(string crs, int timeWindow);

        /// <summary>
        /// Gets service details for the provided service.
        /// </summary>
        /// <param name="serviceID">ID of the service.</param>
        /// <returns></returns>
        Task<ServiceDetails> GetServiceDetailsAsync(string serviceID);

    }
}
