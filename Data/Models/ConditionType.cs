using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class ConditionType
    {
        #region props
        [Key]
        [Required]
        [MaxLength(2)]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Type { get; set; }
        #endregion

        #region lazy props
        public virtual IEnumerable<Script> Scripts { get; set; } = new List<Script>();
        #endregion
    }
}
