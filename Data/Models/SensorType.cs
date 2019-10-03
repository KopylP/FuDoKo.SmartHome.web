using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class SensorType
    {
        #region props
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string TypeName { get; set; }
        #endregion

        #region lazy props
        public virtual IEnumerable<Sensor> Sensors { get; set; } = new List<Sensor>();
        #endregion
    }
}
