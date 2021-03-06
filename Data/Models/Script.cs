﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Data.Models
{
    public class Script
    {
        #region props
        [Key]
        [Required]
        public int Id { get; set; }

        public float? ConditionValue { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(2)]
        public int? ConditionTypeId { get; set; }

        [Required]
        public int ControllerId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime LastModificationDate { get; set; }

        public int? SensorId { get; set; }

        [Required]
        public DateTime TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        [MaxLength(4)]
        public float? Delta { get; set; }

        public int? RepeatTimes { get; set; }
        
        public bool Complited { get; set; }

        public bool Status { get; set; }

        [Required]
        public bool Visible { get; set; }

        [Required]
        [MaxLength(2)]
        public int Priority { get; set; }//default 0
        #endregion

        #region lazy props
        public virtual IEnumerable<Command> Commands { get; set; } = new List<Command>();

        [ForeignKey("SensorId")]
        public virtual Sensor Sensor { get; set; }

        [ForeignKey("ConditionTypeId")]
        public virtual ConditionType ConditionType { get; set; }

        [ForeignKey("ControllerId")]
        public virtual Controller Controller { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } 
        #endregion
    }
}
