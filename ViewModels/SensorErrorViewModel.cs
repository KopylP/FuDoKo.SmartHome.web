using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class SensorErrorViewModel
    {
        public SensorViewModel Sensor { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
