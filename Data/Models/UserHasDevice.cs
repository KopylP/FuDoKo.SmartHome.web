using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class UserHasDevice
    {
        #region props
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int UsersHaveControllerId { get; set; }

        [Required]
        public int DeviceId { get; set; }
        #endregion

        #region lazy props
        [ForeignKey("UsersHaveControllerId")]
        public virtual UserHasController UserHasController { get; set; }

        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }
        #endregion
    }
}
