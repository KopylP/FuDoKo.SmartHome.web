using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class ControllerAccessViewModel
    {
        [Required]
        public int ControllerId { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
