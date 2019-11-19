using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class Device
    {
        #region props
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public int Pin { get; set; }

        [StringLength(16)]
        public string MAC { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public int ControllerId { get; set; }

        [Required]
        public int DeviceTypeId { get; set; }

        #endregion
        #region lazy props
        [ForeignKey("ControllerId")]
        public virtual Controller Controller { get; set; }

        [ForeignKey("DeviceTypeId")]
        public virtual DeviceType DeviceType { get; set; }

        public virtual IEnumerable<UserHasDevice> UsersHaveDevice { get; set; } = new List<UserHasDevice>();

        public virtual IEnumerable<DeviceConfiguration> DeviceConfigurations { get; set; }
        #endregion
    }
}
