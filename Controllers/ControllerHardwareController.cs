using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuDoKo.SmartHome.web.Api.ApiErrors;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.Extensions;
using FuDoKo.SmartHome.web.Filters;
using FuDoKo.SmartHome.web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuDoKo.SmartHome.web.Controllers
{
    [ServiceFilter(typeof(ControllerAuthFilter))]
    public class ControllerHardwareController : BaseApiController
    {
        public ControllerHardwareController(ApplicationDbConrext context) : base(context)
        {

        }
        /// <summary>
        /// нужные заголовки:
        /// fudoko-mac-address: your-controller-mac-address
        /// Метод возващает доступные сенсоры для контроллера
        /// </summary>
        /// <returns></returns>
        [HttpGet("Sensors")]
        public IActionResult Sensors()
        {
            var macAddress = HttpContext.Mac();
            var controller = _context.Controllers.Where(p => p.MAC == macAddress).FirstOrDefault();
            if (controller == null) return Unauthorized(new UnauthorizedError());
            var sensors = _context.Sensors
                .Include(p => p.SensorType)
                .Where(p => p.ControllerId == controller.Id)
                .Where(p => p.Status);
            return Json(sensors.Adapt<SensorViewModel[]>());
        }
        /// <summary>
        /// нужные заголовки:
        /// fudoko-mac-address: your-controller-mac-address
        /// Изменение значения датчика в таблице
        /// </summary>
        /// <returns></returns>
        [HttpPost("Sensors")]
        public async Task<IActionResult> UpdateSensors([FromBody]SensorViewModel model)
        {
            
            var controller = _context.Controllers.Where(p => p.MAC == HttpContext.Mac()).FirstOrDefault();
            if (controller == null) return Unauthorized();
            var sensor = _context.Sensors
                .Where(p => p.Id == model.Id)
                .Where(p => p.ControllerId == controller.Id)
                .Where(p => p.Status)
                .FirstOrDefault();
            if (sensor == null) return NotFound(new NotFoundError());
            sensor.Value = model.Value;
            _context.Sensors.Update(sensor);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return Json(sensor.Adapt<SensorViewModel>());
        }
    }
}