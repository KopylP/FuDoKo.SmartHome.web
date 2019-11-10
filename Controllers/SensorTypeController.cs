using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuDoKo.SmartHome.web.Controllers
{
    [Authorize]
    public class SensorTypeController : BaseApiController
    {
        public SensorTypeController(ApplicationDbConrext context) : base(context)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var sensorTypes = _context.SensorTypes.ToArray();
            return Json(sensorTypes.Adapt<SensorTypeViewModel[]>());
        }
    }
}
