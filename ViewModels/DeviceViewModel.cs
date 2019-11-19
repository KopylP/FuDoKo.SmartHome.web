using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class DeviceViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Pin { get; set; }

        [StringLength(16)]
        public string MAC { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public int ControllerId { get; set; }

        [Required]
        public int DeviceTypeId { get; set; }

        public DeviceTypeViewModel DeviceType { get; set; }
    }
}
