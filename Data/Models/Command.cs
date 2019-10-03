using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class Command
    {
        #region props
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ScriptId { get; set; }

        [Required]
        public DateTime Time { get; set; }
        
        [Required]
        public int DeviceConfigurationId { get; set; }

        [Required]
        public string Name { get; set; }
        #endregion

        #region lazy props
        [ForeignKey("DeviceConfigurationId")]
        public virtual DeviceConfiguration DeviceConfiguration { get; set; }
        
        [ForeignKey("ScriptId")]
        public virtual Script Script { get; set; }
        #endregion
    }
}
