using System;
using System.Collections.Generic;
using System.Linq;
using UsageService.BusinessContract;
using UsageService.Entities;

namespace UsageService.Business
{
    public class DailyUsage : IDailyUsage
    {
        public IEnumerable<AverageReading> Average(List<UsageReading> usageReadings)
        {
            if(usageReadings.Count == 1)
            {
                List<AverageReading> monoUsageReading = new List<AverageReading>();
                monoUsageReading.Add(new AverageReading
                {
                    SerialNumber = usageReadings[0].SerialNumber,
                    Reading = "Unknown"
                });
                return monoUsageReading;
            }
            else
            {
                Dictionary<string, List<UsageReading>> deviceUsageReadingsDictionary = new Dictionary<string, List<UsageReading>>();
                foreach (var item in usageReadings)
                {
                    List<UsageReading> usageReadingsByDevice;
                    if (deviceUsageReadingsDictionary.TryGetValue(item.SerialNumber,out usageReadingsByDevice))
                    {
                        //Assuming No duplicate readings, to be refactored
                        usageReadingsByDevice.Add(item);
                    }
                    else
                    {
                        deviceUsageReadingsDictionary[item.SerialNumber] = new List<UsageReading>();
                        deviceUsageReadingsDictionary[item.SerialNumber].Add(item);
                    }
                }
                List<AverageReading> averageReadings = new List<AverageReading>();
                foreach (var deviceReadingItem in deviceUsageReadingsDictionary)
                {
                    if(deviceReadingItem.Value.Count <= 1)
                    {
                        averageReadings.Add(new AverageReading
                        {
                            SerialNumber = deviceReadingItem.Key,
                            Reading = "Unknown"
                        });
                    }
                    else
                    {
                        var orderedReadings = deviceReadingItem.Value.OrderBy(x => x.TimeStamp);
                        DateTime previousReadingTimeStamp = new DateTime();
                        DateTime currentReadingTimeStamp;
                        double previousReadingBatteryLevel = double.MinValue;
                        double currentReadingBatteryLevel;
                        double average;
                        foreach (var item in orderedReadings)
                        {
                            currentReadingTimeStamp = item.TimeStamp;
                            currentReadingBatteryLevel = item.BatteryLevel;
                            if (previousReadingTimeStamp != new DateTime() && previousReadingBatteryLevel != double.MinValue)
                            {
                                TimeSpan usageHours = currentReadingTimeStamp - previousReadingTimeStamp;
                                double dischargeOfBattery = currentReadingBatteryLevel * 100 - previousReadingBatteryLevel * 100;

                                if (usageHours.TotalHours != 24)
                                {
                                    average = ((dischargeOfBattery) / usageHours.TotalHours) * 24;
                                    averageReadings.Add(new AverageReading
                                    {
                                        SerialNumber = item.SerialNumber,
                                        Reading = average
                                    });
                                }
                            }
                            previousReadingTimeStamp = currentReadingTimeStamp;
                            currentReadingTimeStamp = new DateTime();
                            previousReadingBatteryLevel = currentReadingBatteryLevel;
                            currentReadingBatteryLevel = double.MinValue;
                        }
                    }
                    
                }

                return averageReadings;
            }
        }

    }
}
