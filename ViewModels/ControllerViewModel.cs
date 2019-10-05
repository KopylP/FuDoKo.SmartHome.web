using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class ControllerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MAC { get; set; }

        public bool Status { get; set; }
    }
}
