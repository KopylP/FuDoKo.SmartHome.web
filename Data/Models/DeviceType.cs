using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class DeviceType
    {
        #region props
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeName { get; set; }
        #endregion

        #region lazy props
        public virtual IEnumerable<Measure> Measures { get; set; } = new List<Measure>();

        public virtual IEnumerable<Device> Devices { get; set; } = new List<Device>();
        #endregion

    }
}
