using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FuDoKo.SmartHome.web.Api.ApiErrors;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.Data.Models;
using FuDoKo.SmartHome.web.Extensions;
using FuDoKo.SmartHome.web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuDoKo.SmartHome.web.Controllers
{

    public class AccessController : BaseApiController
    {
        public AccessController(ApplicationDbConrext context) : base(context)
        {
        }

        #region access controrllers
        [HttpGet("Controller/{id}")]
        public IActionResult AccessToController(int id)
        {
            var user = User.GetUser(_context);

            var userHasController = _context.UserHasControllers
                .Where(p => p.UserId == user.Id)
                .Where(p => p.IsAdmin)
                .Where(p => p.ControllerId == id)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var usersHasThisController = _context
                .UserHasControllers
                .Include(p => p.User)
                .Where(p => p.ControllerId == id)
                .Where(p => !p.IsAdmin);

            return Json(usersHasThisController.Adapt<UserHasControllerViewModel[]>());
        }

        [HttpPut("Controller")]
        public IActionResult AccessToController([FromBody]ControllerAccessViewModel model)
        {
            if (model == null) return StatusCode(500, new InternalServerError());
            
            var user = User.GetUser(_context);

            var userHasController = _context
                .UserHasControllers.Where(p => p.UserId == user.Id)
                .Where(p => p.ControllerId == model.ControllerId)
                .Where(p => p.IsAdmin)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var accessUser = _context
                .Users
                .Where(p => p.Email.ToLower(CultureInfo.CurrentCulture) == model.UserName.ToLower(CultureInfo.CurrentCulture) || p.UserName == model.UserName)
                .FirstOrDefault();

            if (accessUser == null) return NotFound(new NotFoundError("User not found!"));

            var accessUserHasControler = _context
                .UserHasControllers.Where(p => p.UserId == accessUser.Id)
                .Where(p => p.ControllerId == model.ControllerId)
                .FirstOrDefault();

            if (accessUserHasControler != null) return StatusCode(500, new InternalServerError("User has already added!"));

            var accessUserHasController = new UserHasController
            {
                IsAdmin = false,
                ControllerId = model.ControllerId,
                UserId = accessUser.Id
            };

            _context.UserHasControllers.Add(accessUserHasController);
            _context.SaveChanges();
            return Json(accessUserHasController.Adapt<UserHasControllerViewModel>());
        }

        [HttpDelete("Controller/{id}")]
        public IActionResult AccessToControllerDelete(int id)
        {
            var user = User.GetUser(_context);

            var accessUserHasController = _context.UserHasControllers.Find(id);

            if (accessUserHasController == null) return NotFound(new NotFoundError());

            var userHasController = _context.UserHasControllers
                .Where(p => p.ControllerId == accessUserHasController.ControllerId)
                .Where(p => p.UserId == user.Id)
                .Where(p => p.IsAdmin)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            _context.UserHasControllers.Remove(accessUserHasController);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region access devices
        [HttpGet("Device/{id}")]
        public IActionResult AccessDevice(int id)
        {
            var user = User.GetUser(_context);

            var device = _context.Devices.Find(id);

            if (device == null) return NotFound(new NotFoundError());

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.UserId == user.Id)
                .Where(p => p.ControllerId == device.ControllerId)
                .Where(p => p.IsAdmin)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var accessUserHasDevice = _context
                .UserHasDevices
                .Where(p => p.DeviceId == id)
                .Where(p => p.UsersHaveControllerId != userHasController.Id)
                .Include(p => p.UserHasController)
                .ThenInclude(p => p.User);
            //TODO ADAPT
            return Json(accessUserHasDevice);
        }

        [HttpPut("Device")]
        public IActionResult AccessDevice([FromBody] DeviceAccessViewModel model)
        {
            var user = User.GetUser(_context);

            var device = _context.Devices.Find(model.DeviceId);

            if (device == null) return NotFound(new NotFoundError("Device not found!"));

            var accessUser = _context
                .Users
                .Where(p => p.Email == model.UserName || p.UserName == model.UserName)
                .FirstOrDefault();

            if (accessUser == null) return NotFound(new NotFoundError("A user with such login or email does not exist."));

            var userHasController = _context.UserHasControllers
                .Where(p => p.ControllerId == device.ControllerId)
                .Where(p => p.UserId == user.Id)
                .Where(p => p.IsAdmin)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var accessUserHasController = _context.UserHasControllers
                .Where(p => p.ControllerId == device.ControllerId)
                .Where(p => p.UserId == user.Id)
                .FirstOrDefault();

            if (accessUserHasController == null) return Unauthorized(new UnauthorizedError("User hasn`t access to your controller!"));

            var accessUserHasDevice = new UserHasDevice
            {
                DeviceId = device.Id,
                UsersHaveControllerId = accessUserHasController.Id
            };

            _context.UserHasDevices.Add(accessUserHasDevice);
            _context.SaveChanges();
            accessUserHasDevice.UserHasController = _context.UserHasControllers.Find(accessUserHasDevice.UsersHaveControllerId);
            accessUserHasDevice.UserHasController.User = _context.Users.Find(accessUserHasDevice.UserHasController.UserId);
            //TODO ADAPT
            return Json(accessUserHasDevice);
        }
        #endregion

    }
}