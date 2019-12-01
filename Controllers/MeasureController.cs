using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuDoKo.SmartHome.web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuDoKo.SmartHome.web.Controllers
{
    [Authorize]
    public class MeasureController : BaseApiController
    {
        public MeasureController(ApplicationDbConrext context) : base(context)
        {
        }

        [HttpGet("{deviceTypeId}")]
        public IActionResult Get(int deviceTypeId)
        {
            var measures = _context.Measures.Where(p => p.DeviceTypeId == deviceTypeId);
            return Json(measures.ToArray());
        }
    }
}