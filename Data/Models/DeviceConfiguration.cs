using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class DeviceConfiguration
    {
        #region props
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        public int Value { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required]
        public int MeasureId { get; set; }
        #endregion

        #region lazy props
        [ForeignKey("DeviceId")]
        public virtual Device  Device { get; set; }

        [ForeignKey("MeasureId")]
        public virtual Measure Measure { get; set; }

        public virtual IEnumerable<Command> Commands { get; set; } = new List<Command>();
        #endregion
    }
}
