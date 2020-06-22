using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UsageService.Business;
using UsageService.Entities;

namespace UsageService.UnitTests.Buisiness
{
    [TestFixture]
    public class AverageUsageTest
    {
        [Test]
        public void When_Only_One_Reading_Is_Available_In_Collection_Returns_Battery_Usage_Unknown()
        {
            List<UsageReading> usageReadings = new List<UsageReading>();
            usageReadings.Add(new UsageReading
            {
                AcademyId = 30006,
                BatteryLevel = 0.68,
                EmployeeId = "T1007384",
                SerialNumber = "1805C67HD02259",
                TimeStamp = new DateTime(2020,08,01,9,0,0)
            });

            DailyUsage dailyUsage = new DailyUsage();
            IEnumerable<AverageReading> output = dailyUsage.Average(usageReadings);
            Assert.AreEqual("Unknown", output.Select(x => x.Reading).FirstOrDefault());
        }
        [Test]
        public void When_Only_Two_Consecutive_Readings_Are_Available_In_Collection_For_A_Same_Device_Returns_Battery_Usage_Average()
        {
            List<UsageReading> usageReadings = new List<UsageReading>();
            usageReadings.Add(new UsageReading
            {
                AcademyId = 30006,
                BatteryLevel = 1,
                EmployeeId = "T1007384",
                SerialNumber = "1805C67HD02259",
                TimeStamp = Convert.ToDateTime("2019-06-19T04:30:00.000+01:00")
            });
            usageReadings.Add(new UsageReading
            {
                AcademyId = 30006,
                BatteryLevel = 0.90,
                EmployeeId = "T1007384",
                SerialNumber = "1805C67HD02259",
                TimeStamp = Convert.ToDateTime("2019-06-18T16:30:00.000+01:00")
            });
            var test = (Convert.ToDateTime("2019-06-19T16:30:00.000+01:00") - Convert.ToDateTime("2019-06-18T04:30:00.000+01:00")).TotalHours;
            DailyUsage dailyUsage = new DailyUsage();
            IEnumerable<AverageReading> output = dailyUsage.Average(usageReadings);
            Assert.AreEqual(20, output.Select(x => x.Reading).FirstOrDefault());
        }
        
    }
}
