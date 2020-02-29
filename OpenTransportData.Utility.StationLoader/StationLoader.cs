using OpenTransportData.Utility.StationLoader.Models;
using System.Collections.Generic;
using System.IO;

namespace OpenTransportData.Utility.StationLoader
{
    public class StationLoader : IStationLoader
    {
        private readonly string _filePath = "./Static/station_codes.csv";
        private static IEnumerable<Station> _stations { get; set; }

        /// <summary>
        /// Load all stations from the data source.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetAll()
        {
            if(_stations == null)
            {
                var stationList = new List<Station>();
                using(var reader = new StreamReader(_filePath))
                {
                    while(!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        stationList.Add(new Station()
                        {
                            Name = values[0],
                            CRS = values[1]
                        });
                    }
                }

               _stations = stationList;
            }

            return _stations;
        }
    }
}
