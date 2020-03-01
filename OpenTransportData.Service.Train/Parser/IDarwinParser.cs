using NationalRailDarwin;
using OpenTransportData.Core.Enums;
using OpenTransportData.Service.Train.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenTransportData.Service.Train.Parser
{
    public interface IDarwinParser
    {
        /// <summary>
        /// Parses a NationalRail ServiceItem1 object into a TimetableEntry model.
        /// </summary>
        /// <param name="details">Details to parse.</param>
        /// <returns></returns>
        IEnumerable<TimetableEntry> Parse(IEnumerable<ServiceItem1> items, TimetableTypes timetableType);

        /// <summary>
        /// Parses a NationalRail ServiceItemWithCallingPoints object into a TimetableEntryDetail model.
        /// </summary>
        /// <param name="details">Details to parse.</param>
        /// <returns></returns>
        IEnumerable<TimetableEntryDetail> Parse(IEnumerable<ServiceItemWithCallingPoints1> items, TimetableTypes timetableType);

        /// <summary>
        /// Parses a NationalRail ServiceDetails object into a ServiceDetails model.
        /// </summary>
        /// <param name="details">Details to parse.</param>
        /// <returns></returns>
        Models.ServiceDetails Parse(NationalRailDarwin.ServiceDetails details);
    }
}
