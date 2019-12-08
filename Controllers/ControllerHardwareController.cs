﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuDoKo.SmartHome.web.Api.ApiErrors;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.Extensions;
using FuDoKo.SmartHome.web.Filters;
using FuDoKo.SmartHome.web.Hubs;
using FuDoKo.SmartHome.web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FuDoKo.SmartHome.web.Controllers
{
    [ServiceFilter(typeof(ControllerAuthFilter))]
    public class ControllerHardwareController : BaseApiController
    {
        private IHubContext<SensorHub, ITypedHubClient> _hubContext;
        public ControllerHardwareController(ApplicationDbConrext context, IHubContext<SensorHub, ITypedHubClient> hubContext) : base(context)
        {
            _hubContext = hubContext;
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
            if (controller == null) return Unauthorized(new UnauthorizedError());
            var userHasController = _context.UserHasControllers
                .Where(p => p.ControllerId == controller.Id)
                .FirstOrDefault();
            if (userHasController == null) return StatusCode(500, new InternalServerError());
            var sensor = _context.Sensors
                .Include(p => p.SensorType)
                .Where(p => p.Id == model.Id)
                .Where(p => p.ControllerId == controller.Id)
                .Where(p => p.Status)
                .FirstOrDefault();
            if (sensor == null) return NotFound(new NotFoundError());
            sensor.Value = model.Value;
            _context.Sensors.Update(sensor);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            var sensorViewModel = sensor.Adapt<SensorViewModel>();

            var userHasControllers = _context.UserHasControllers
                .Where(p => p.ControllerId == controller.Id);

            foreach(var usrHasCntrl in userHasControllers)
            {
                await _hubContext.Clients.User(usrHasCntrl.UserId).UpdateSensor(sensorViewModel).ConfigureAwait(false);
            }
            return Json(sensor.Adapt<SensorViewModel>());
        }

        [Obsolete]
        [HttpGet("Scripts")]
        public async Task<IActionResult> GetScripts()
        {
            var controller = _context.Controllers.Where(p => p.MAC == HttpContext.Mac()).FirstOrDefault();
            if (controller == null) return Unauthorized(new UnauthorizedError());

            var scripts = await _context.Scripts
                .Where(p => p.ControllerId == controller.Id)
                .Where(p => !p.Complited)
                .Where(p => p.Status)
                .Include(p => p.ConditionType)
                .Include(p => p.Commands)
                .ThenInclude(p => p.DeviceConfiguration)
                .ThenInclude(p => p.Device)
                .Include(p => p.Commands)
                .ThenInclude(p => p.DeviceConfiguration)
                .ThenInclude(p => p.Measure)
                .ToArrayAsync().ConfigureAwait(false);

            return Json(scripts.Adapt<ScriptViewModel[]>());
        }

        [HttpGet("Scripts/Ids")]
        public async Task<IActionResult> GetScriptsIds()
        {
            var controller = _context.Controllers.Where(p => p.MAC == HttpContext.Mac()).FirstOrDefault();
            if (controller == null) return Unauthorized(new UnauthorizedError());

            var scriptsIds = await _context.Scripts
                .Where(p => p.ControllerId == controller.Id)
                .Where(p => !p.Complited)
                .Where(p => p.Status)
                .Select(p => p.Id)
                .ToArrayAsync().ConfigureAwait(false);

            return Json(scriptsIds);
        }

        [HttpGet("Scripts/{scriptId}")]
        public async Task<IActionResult> GetScript(int scriptId)
        {
            var controller = _context.Controllers.Where(p => p.MAC == HttpContext.Mac()).FirstOrDefault();
            if (controller == null) return Unauthorized(new UnauthorizedError());

            var script = await _context.Scripts
                .Where(p => p.ControllerId == controller.Id)
                .Where(p => !p.Complited)
                .Where(p => p.Status)
                .Where(p => p.Id == scriptId)
                .Include(p => p.ConditionType)
                .Include(p => p.Commands)
                .ThenInclude(p => p.DeviceConfiguration)
                .ThenInclude(p => p.Device)
                .Include(p => p.Commands)
                .ThenInclude(p => p.DeviceConfiguration)
                .ThenInclude(p => p.Measure)
                .FirstOrDefaultAsync().ConfigureAwait(false);
            return Json(script.Adapt<ScriptViewModel>());
        }


        [HttpDelete("Scripts/{scriptId}")]
        public async Task<IActionResult> DeleteScript(int scriptId)
        {
            var controller = _context.Controllers.Where(p => p.MAC == HttpContext.Mac()).FirstOrDefault();
            if (controller == null) return Unauthorized(new UnauthorizedError());

            var script = await _context.Scripts.FindAsync(scriptId).ConfigureAwait(false);

            if (script == null) return NotFound(new NotFoundError());

            return NoContent();
        }

    }

}