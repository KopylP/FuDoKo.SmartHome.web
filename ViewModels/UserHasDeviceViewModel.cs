using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class UserHasDeviceViewModel
    {
        public int Id { get; set; }

        public int UsersHaveControllerId { get; set; }

        public int DeviceId { get; set; }


        public virtual UserHasControllerViewModel UserHasController { get; set; }

        public virtual DeviceViewModel Device { get; set; }
    }
}
