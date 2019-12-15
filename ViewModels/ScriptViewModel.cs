using FuDoKo.SmartHome.web.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class ScriptViewModel
    {
        public int Id { get; set; }

        public float? ConditionValue { get; set; }

        [Required]
        public string Name;
        public int? ConditionTypeId { get; set; }
        public int? SensorId { get; set; }

        public bool Status { get; set; }
        public int ControllerId { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        public float? Delta { get; set; }

        public int RepeatTimes { get; set; }

        public int Priority { get; set; }//default 0

        public virtual SensorViewModel Sensor { get; set; }

        public virtual ConditionTypeViewModel ConditionType { get; set; }

        public virtual List<CommandViewModel> Commands { get; set; }
    }
}
