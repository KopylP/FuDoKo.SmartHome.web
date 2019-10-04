using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class Controller
    {
        #region props
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Key]
        [StringLength(16)]
        public string MAC { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime InstalledDate { get; set; }

        [Required]
        public DateTime LastConnection { get; set; }

        [Required]
        public string PublicKey { get; set; }
        #endregion

        #region lazy props
        public virtual IEnumerable<Sensor> Sensors { get; set; } = new List<Sensor>();
        public virtual IEnumerable<UserHasController> UsersHaveController { get; set; } = new List<UserHasController>(); 
        #endregion
    }
}
