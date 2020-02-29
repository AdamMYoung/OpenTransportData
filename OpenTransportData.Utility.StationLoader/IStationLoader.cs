using OpenTransportData.Utility.StationLoader.Models;
using System.Collections.Generic;

namespace OpenTransportData.Utility.StationLoader
{
    public interface IStationLoader
    {
        /// <summary>
        /// Load all stations from the data source.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Station> GetAll();
    }
}