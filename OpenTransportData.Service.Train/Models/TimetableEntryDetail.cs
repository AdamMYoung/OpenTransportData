using OpenTransportData.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenTransportData.Service.Train.Models
{
    public class TimetableEntryDetail
    {
        /// <summary>
        /// ID of the entry.
        /// </summary>
        public string ServiceID { get; set; }

        /// <summary>
        /// Origin of the train.
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Destination of the train.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Platform the train will depart from.
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Time the train is planned to depart/arrive at.
        /// </summary>
        public DateTime? PlannedTime { get; set; }

        /// <summary>
        /// Time the train will depart/arrive at.
        /// </summary>
        public DateTime? EstimatedTime { get; set; }

        /// <summary>
        /// Carriage length of the train.
        /// </summary>
        public int? Length { get; set; }

        /// <summary>
        /// Name of the operator of the train.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Optional reason the train has been delayed.
        /// </summary>
        public string DelayReason { get; set; }

        /// <summary>
        /// Optional reason the train has been canceled.
        /// </summary>
        public string CancellationReason { get; set; }

        /// <summary>
        /// Current status of the train.
        /// </summary>
        public TravelStatus Status { get; set; }

        /// <summary>
        /// Previous calling points of the timetable.
        /// </summary>
        public IEnumerable<CallingPoint> PreviousCallingPoints { get; set; }

        /// <summary>
        /// Subsequent calling points of the timetable.
        /// </summary>
        public IEnumerable<CallingPoint> SubsequentCallingPoint { get; set; }
    }
}
