using OpenTransportData.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenTransportData.Service.Train.Models
{
    public class CallingPoint
    {
        /// <summary>
        /// Name of the location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// CRS code of the location.
        /// </summary>
        public string CRS { get; set; }

        /// <summary>
        /// Scheduled time of the service.
        /// </summary>
        public DateTime PlannedTime { get; set; }

        /// <summary>
        /// Estimated time of the service.
        /// </summary>
        public DateTime? EstimatedTime { get; set; }

        /// <summary>
        /// Length of the train.
        /// </summary>
        public int? Length { get; set; }
    }
}
