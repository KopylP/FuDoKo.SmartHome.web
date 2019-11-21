using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class UserHasControllerViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public int ControllerId { get; set; }

        public bool IsAdmin { get; set; }

        public virtual UserViewModel User { get; set; }
    }
}
