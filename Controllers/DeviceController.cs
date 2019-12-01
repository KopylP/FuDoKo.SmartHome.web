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
    [Authorize]
    public class DeviceController : BaseApiController
    {
        #region constructor
        public DeviceController(ApplicationDbConrext context) : base(context)
        {
        }
        #endregion

        #region methods
        [HttpGet("All/{controllerId}")]
        public IActionResult All(int controllerId, [FromQuery(Name = "virtual")] bool withVirtual = false)
        {
            var user = User.GetUser(_context);

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.ControllerId == controllerId)
                .Where(p => p.UserId == user.Id)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedResult());

            var userHasDevices = _context.UserHasDevices
                .Include(p => p.Device)
                .Where(p => p.UsersHaveControllerId == userHasController.Id);

            var devices = userHasDevices
                .Select(p => p.Device)
                .Include(p => p.DeviceType)
                .AsQueryable();

            if (!withVirtual)
            {
                devices = devices.Where(p => p.DeviceType.TypeName != "Virtual");
            }

            return Json(devices.Adapt<DeviceViewModel[]>());
        }
        #endregion

        #region rest methods
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = User.GetUser(_context);

            var device = _context.Devices
                .Include(p => p.DeviceType)
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (device == null) return NotFound(new NotFoundError());

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.UserId == user.Id)
                .Where(p => p.ControllerId == device.ControllerId)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var userHasDevice = _context
                .UserHasDevices
                .Where(p => p.UsersHaveControllerId == userHasController.Id)
                .Where(p => p.DeviceId == id)
                .FirstOrDefault();

            if (userHasDevice == null) return Unauthorized(new UnauthorizedError());

            return Json(device.Adapt<DeviceViewModel>());
        }

        [HttpPost]
        public IActionResult Post([FromBody]DeviceViewModel model)
        {
            if (model == null) return StatusCode(500, new InternalServerError());

            if (model.MAC != null && model.MAC.Length != 12) return StatusCode(500, new InternalServerError("MAС address must be 12 characters long!"));

            var user = User.GetUser(_context);

            var device = _context.Devices
                .Include(p => p.DeviceType)
                .Where(p => p.Id == model.Id)
                .FirstOrDefault();

            if (device == null) return NotFound(new NotFoundResult());

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.UserId == user.Id)
                .Where(p => p.ControllerId == device.ControllerId)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedResult());

            var userHasDevice = _context
                .UserHasDevices
                .Where(p => p.UsersHaveControllerId == userHasController.Id)
                .Where(p => p.DeviceId == device.Id)
                .FirstOrDefault();

            if (userHasDevice == null) return Unauthorized(new UnauthorizedError());

            var devicesWithCommonMacAddress = _context
                .Devices
                .Where(p => p.ControllerId == userHasController.ControllerId)
                .Where(p => p.MAC != null && p.MAC != device.MAC && p.MAC == model.MAC);

            if (devicesWithCommonMacAddress.Any()) return StatusCode(500, new InternalServerError("Mac Address is already exists!"));

            var devicesWithCommonPin = _context
                .Devices
                .Where(p => p.ControllerId == userHasController.ControllerId)
                .Where(p => p.Pin != 0 && p.Pin != device.Pin && p.Pin == model.Pin);

            var sensorsWithCommonPin = _context
                .Sensors
                .Where(p => p.ControllerId == userHasController.ControllerId)
                .Where(p => p.Pin != 0 && p.Pin != device.Pin && p.Pin == model.Pin);

            if (devicesWithCommonPin.Any() || sensorsWithCommonPin.Any())
                return StatusCode(500, new InternalServerError("Pin is already taken!"));

            device.Name = model.Name;
            device.MAC = model.MAC;
            device.Pin = model.Pin;
            device.Status = model.Status;
            _context.Devices.Update(device);
            _context.SaveChanges();
            return Json(device.Adapt<DeviceViewModel>());
        }

        [HttpPut]
        public IActionResult Put([FromBody]DeviceViewModel model)
        {
            if (model == null) return StatusCode(500, new InternalServerError());

            if (model.MAC != null && model.MAC.Length != 12) return StatusCode(500, new InternalServerError("MAС address must be 12 characters long!"));

            var user = User.GetUser(_context);

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.ControllerId == model.ControllerId)
                .Where(p => p.UserId == user.Id)
                .FirstOrDefault();

            if (userHasController == null || !userHasController.IsAdmin) return Unauthorized(new UnauthorizedError());

            var devicesWithCommonMacAddress = _context
                .Devices
                .Where(p => p.ControllerId == userHasController.ControllerId)
                .Where(p => p.MAC != null && p.MAC == model.MAC);

            if (devicesWithCommonMacAddress.Any()) return StatusCode(500, new InternalServerError("Mac Address is already exists!"));

            var devicesWithCommonPin = _context
                .Devices
                .Where(p => p.ControllerId == userHasController.ControllerId)
                .Where(p => p.Pin != 0 && p.Pin == model.Pin);

            var sensorsWithCommonPin = _context
                .Sensors
                .Where(p => p.ControllerId == userHasController.ControllerId)
                .Where(p => p.Pin != 0 && p.Pin == model.Pin);

            if (devicesWithCommonPin.Any() || sensorsWithCommonPin.Any())
                return StatusCode(500, new InternalServerError("Pin is already taken!"));

            var device = new Device()
            {
                Name = model.Name,
                DeviceTypeId = model.DeviceTypeId,
                Pin = model.Pin,
                MAC = model.MAC,
                ControllerId = model.ControllerId,
                Status = model.Status
            };

            _context.Devices.Add(device);

            var userHasDevice = new UserHasDevice
            {
                UsersHaveControllerId = userHasController.Id,
                DeviceId = device.Id
            };

            _context.UserHasDevices.Add(userHasDevice);

            _context.SaveChanges();

            device.DeviceType = _context.DeviceTypes.Find(device.DeviceTypeId);

            return Json(device.Adapt<DeviceViewModel>());
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = User.GetUser(_context);

            var device = _context.Devices.Find(id);

            if (device == null) return NotFound(new UnauthorizedError());

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.UserId == user.Id)
                .Where(p => p.ControllerId == device.ControllerId)
                .FirstOrDefault();

            if (userHasController == null || !userHasController.IsAdmin) return Unauthorized(new UnauthorizedError());

            _context.Devices.Remove(device);
            _context.SaveChanges();

            return NoContent();
        }
        #endregion
    }
}
