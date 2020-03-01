using System;
using System.Collections.Generic;
using System.Text;

namespace OpenTransportData.Service.Train.Models
{
    public class ServiceDetails
    {
        public DateTime GeneratedAt { get; set; }
        public string LocationName { get; set; }
        public string CRS { get; set; }
        public string Operator { get; set; }
        public string OperatorCode { get; set; }
        public int Length { get; set; }
        public string Platform { get; set; }
        public DateTime? PlannedTime { get; set; }
        public DateTime? EstimatedTime { get; set; }
        public IEnumerable<CallingPoint> PreviousCallingPoints { get; set; }
        public IEnumerable<CallingPoint> SubsequentCallingPoints { get; set; }
    }
}
