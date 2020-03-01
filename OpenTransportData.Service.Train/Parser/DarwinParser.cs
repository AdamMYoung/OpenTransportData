using NationalRailDarwin;
using OpenTransportData.Core.Enums;
using OpenTransportData.Service.Train.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTransportData.Service.Train.Parser
{
    public class DarwinParser : IDarwinParser
    {
        /// <summary>
        /// Parses a NationalRail ServiceItem1 object into a TimetableEntry model.
        /// </summary>
        /// <param name="details">Details to parse.</param>
        /// <returns></returns>
        public IEnumerable<TimetableEntry> Parse(IEnumerable<ServiceItem1> items, TimetableTypes timetableType)
        {
            return items.Select(t => new TimetableEntry()
            {
                ServiceID = t.serviceID,
                Origin = t.origin[0].locationName,
                Destination = t.destination[0].locationName,
                Platform = t.platform,
                PlannedTime = DateTime.Parse(t.sta),
                EstimatedTime = GetEstimatedTime(t.sta, t.eta),
                Length = t.length,
                Operator = t.@operator,
                DelayReason = t.delayReason,
                CancellationReason = t.cancelReason,
                Status = GetServiceStatus(t, timetableType)
            });
        }

        /// <summary>
        /// Parses a NationalRail ServiceItemWithCallingPoints object into a TimetableEntryDetail model.
        /// </summary>
        /// <param name="details">Details to parse.</param>
        /// <returns></returns>
        public IEnumerable<TimetableEntryDetail> Parse(IEnumerable<ServiceItemWithCallingPoints1> items, TimetableTypes timetableType)
        {
            return items.Select(t => new TimetableEntryDetail()
            {
                ServiceID = t.serviceID,
                Origin = t.origin[0].locationName,
                Destination = t.destination[0].locationName,
                Platform = t.platform,
                PlannedTime = DateTime.Parse(t.sta),
                EstimatedTime = GetEstimatedTime(t.sta, t.eta),
                Length = t.length,
                Operator = t.@operator,
                DelayReason = t.delayReason,
                CancellationReason = t.cancelReason,
                Status = GetServiceStatus(t, timetableType),
                PreviousCallingPoints = t.previousCallingPoints?.First().callingPoint?.ToList().Select(c => new Models.CallingPoint()
                {
                    Location = c.locationName,
                    CRS = c.crs,
                    PlannedTime = DateTime.Parse(c.st),
                    EstimatedTime = GetEstimatedTime(c.st, c.et),
                    Length = c.length
                }),
                SubsequentCallingPoint = t.subsequentCallingPoints?.First().callingPoint?.ToList().Select(c => new Models.CallingPoint()
                {
                    Location = c.locationName,
                    CRS = c.crs,
                    PlannedTime = DateTime.Parse(c.st),
                    EstimatedTime = GetEstimatedTime(c.st, c.et),
                    Length = c.length
                })
            });
        }

        /// <summary>
        /// Parses a NationalRail ServiceDetails object into a ServiceDetails model.
        /// </summary>
        /// <param name="details">Details to parse.</param>
        /// <returns></returns>
        public Models.ServiceDetails Parse(NationalRailDarwin.ServiceDetails details)
        {
            return new Models.ServiceDetails()
            {
                GeneratedAt = details.generatedAt,
                LocationName = details.locationName,
                Operator = details.@operator,
                OperatorCode = details.operatorCode,
                CRS = details.crs,
                Length = details.length,
                Platform = details.platform,
                PlannedTime = DateTime.Parse(details.sta),
                EstimatedTime = GetEstimatedTime(details.sta, details.eta),
                PreviousCallingPoints = details.previousCallingPoints?.First().callingPoint.ToList().Select(c => new Models.CallingPoint()
                {
                    Location = c.locationName,
                    CRS = c.crs,
                    PlannedTime = DateTime.Parse(c.st),
                    EstimatedTime = GetEstimatedTime(c.st, c.et),
                    Length = c.length
                }),
                SubsequentCallingPoints = details.subsequentCallingPoints?.First()?.callingPoint.ToList().Select(c => new Models.CallingPoint()
                {
                    Location = c.locationName,
                    CRS = c.crs,
                    PlannedTime = DateTime.Parse(c.st),
                    EstimatedTime = GetEstimatedTime(c.st, c.et),
                    Length = c.length
                })
            };
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
            if (estimated == null ||estimated.ToLower() == "on time" || estimated.ToLower() == "cancelled")
                return DateTime.Parse(planned);
            else
                return DateTime.Parse(estimated);
        }
    }
}
