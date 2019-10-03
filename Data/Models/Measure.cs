using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class Measure
    {
        #region props
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string MeasureName { get; set; }
        #endregion

        #region lazy props
        public virtual IEnumerable<DeviceConfiguration> DeviceConfigurations { get; set; }
        #endregion
    }
}
