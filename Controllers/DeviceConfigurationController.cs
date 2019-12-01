using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuDoKo.SmartHome.web.Api.ApiErrors;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.Data.Models;
using FuDoKo.SmartHome.web.Extensions;
using FuDoKo.SmartHome.web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuDoKo.SmartHome.web.Controllers
{
    //TODO security
    [Authorize]
    public class DeviceConfigurationController : BaseApiController
    {
        #region constructor
        public DeviceConfigurationController(ApplicationDbConrext context) : base(context)
        {
        }
        #endregion

        #region REST methods
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var deviceConfiguration = _context
                .DeviceConfigurations
                .Include(p => p.Device)
                .Include(p => p.Measure)
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (deviceConfiguration == null) return NotFound(new NotFoundError());
            //TODO add security
            return Json(deviceConfiguration.Adapt<DeviceConfigurationViewModel>());
        }

        [HttpPost]
        public IActionResult Post([FromBody] DeviceConfigurationViewModel model)
        {
            if (model == null) return StatusCode(500, new InternalServerError());

            var deviceConfiguration = _context.DeviceConfigurations
                .Include(p => p.Device)
                .Include(p => p.Measure)
                .Where(p => p.Id == model.Id)
                .FirstOrDefault();

            if (deviceConfiguration == null) return NotFound(new NotFoundError());

            deviceConfiguration.Value = model.Value;

            return Json(deviceConfiguration.Adapt<DeviceConfigurationViewModel>());
        }

        [HttpPut]
        public IActionResult Put([FromBody] DeviceConfigurationViewModel model)
        {
            if (model == null) return StatusCode(500, new InternalServerError());

            var deviceConfiguration = new DeviceConfiguration
            {
                DeviceId = model.DeviceId,
                MeasureId = model.MeasureId,
                Value = model.Value
            };

            _context.DeviceConfigurations.Add(deviceConfiguration);
            _context.SaveChanges();

            deviceConfiguration.Measure = _context.Measures.Find(deviceConfiguration.MeasureId);
            deviceConfiguration.Device = _context.Devices.Find(deviceConfiguration.DeviceId);

            return Json(deviceConfiguration.Adapt<DeviceConfiguration>());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deviceConfiguration = _context.DeviceConfigurations.Find(id);

            if (deviceConfiguration == null) return NotFound(new NotFoundError());

            _context.DeviceConfigurations.Remove(deviceConfiguration);

            return NoContent();
        }
        #endregion

    }
}
