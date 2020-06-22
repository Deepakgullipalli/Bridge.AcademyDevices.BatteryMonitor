using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BatteryConsumption.Controllers.BatteryUsage
{
    public class DailyController : Controller
    {
        public IActionResult Average()
        {
            return View();
        }
    }
}