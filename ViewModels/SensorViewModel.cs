using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class SensorViewModel
    {
        #region props
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public int Pin { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public int SensorTypeId { get; set; }

        [Required]
        public int ControllerId { get; set; }

        public SensorTypeViewModel SensorType { get; set; }
        #endregion
    }
}
