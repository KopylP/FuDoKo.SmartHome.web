using FuDoKo.SmartHome.web.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class CommandViewModel
    {

        public int Id { get; set; }

        public int ScriptId { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public int DeviceConfigurationId { get; set; }

        public string Name { get; set; }

        public bool End { get; set; }

        public virtual DeviceConfiguration DeviceConfiguration { get; set; }

    }
}
