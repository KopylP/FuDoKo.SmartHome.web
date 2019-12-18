using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        #region props

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(128)]
        public string Surname { get; set; }

        [Required]
        public bool Status { get; set; }

        public string DisplayName { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }
        #endregion

        public string FirebaseToken { get; set; }

        #region lazy props
        public virtual IEnumerable<UserHasController> UserHasControllers { get; set; }

        public virtual IEnumerable<Script> Scripts { get; set; }
        #endregion
    }
}
