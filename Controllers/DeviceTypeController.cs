using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Controllers
{
    [Authorize]
    public class DeviceTypeController : BaseApiController
    {
        #region constructor
        public DeviceTypeController(ApplicationDbConrext context) : base(context)
        {
        }
        #endregion

        #region rest methods
        [HttpGet]
        public IActionResult Get()
        {
            var deviceTypes = _context.DeviceTypes.ToList();
            return Json(deviceTypes.Adapt<DeviceTypeViewModel[]>());
        }
        #endregion
    }
}
