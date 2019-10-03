using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class UserHasController
    {
        #region props
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ControllerId { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
        #endregion

        #region lazy types
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("ControllerId")]
        public virtual Controller Controller { get; set; }
        public virtual IEnumerable<UserHasDevice> UsersHaveDevices { get; set; } =
            new List<UserHasDevice>();
        #endregion
    }
}
