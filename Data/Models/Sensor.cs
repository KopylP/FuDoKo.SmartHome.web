using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class Sensor
    {
        #region props
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(2)]
        public int Pin { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        [MaxLength(3)]
        public int Value { get; set; }

        [Required]
        public int SensorTypeId { get; set; }

        [Required]
        public int ControllerId { get; set; }
        #endregion

        #region lazy props
        [ForeignKey("SensorTypeId")]
        public virtual SensorType SensorType { get; set; }

        [ForeignKey("ControllerId")]
        public virtual Controller Controller { get; set; }

        public virtual IEnumerable<Script> Scripts { get; set; } = new List<Script>();
        #endregion

    }
}
