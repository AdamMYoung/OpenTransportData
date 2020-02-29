using OpenTransportData.Core.Enums;
using System;

namespace OpenTransportData.Service.Train.Models
{
    public class TimetableEntry
    {
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
        public DateTime PlannedTime { get; set; }

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
        /// Optional reason the train has been cancelled.
        /// </summary>
        public string CancellationReason { get; set; }

        /// <summary>
        /// Current status of the train.
        /// </summary>
        public TravelStatus Status { get; set; }
    }
}