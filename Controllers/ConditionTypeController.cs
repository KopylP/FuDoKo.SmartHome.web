using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuDoKo.SmartHome.web.Controllers
{
    [Authorize]
    public class ConditionTypeController : BaseApiController
    {
        public ConditionTypeController(ApplicationDbConrext context) : base(context)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var conditionTypes = _context.ConditionTypes.ToArray();
            return Json(conditionTypes.Adapt<ConditionTypeViewModel[]>());
        }
    }
}