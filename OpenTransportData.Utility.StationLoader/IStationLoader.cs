using OpenTransportData.Utility.StationLoader.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
