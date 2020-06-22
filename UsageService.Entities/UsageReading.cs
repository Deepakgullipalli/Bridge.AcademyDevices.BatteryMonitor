using System;
using System.Collections.Generic;
using System.Text;

namespace UsageService.Entities
{
    public class UsageReading
    {
        public int AcademyId { get; set; }
        public double BatteryLevel { get; set; }
        public string EmployeeId { get; set; }
        public string SerialNumber { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
