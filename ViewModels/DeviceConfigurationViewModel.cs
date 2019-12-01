using FuDoKo.SmartHome.web.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class DeviceConfigurationViewModel
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int DeviceId { get; set; }

        public int MeasureId { get; set; }

        public virtual DeviceViewModel Device { get; set; }

        public virtual Measure Measure { get; set; }
    }
}
