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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuDoKo.SmartHome.web.Controllers
{
    [Authorize]
    public class SensorController : BaseApiController
    {
        public SensorController(ApplicationDbConrext context) : base(context) { }

        [HttpGet("All/{controllerId}")]
        public IActionResult All(int controllerId)
        {
            var user = User.GetUser(_context);
            var userHasController = _context
                .UserHasControllers
                .Where(p => p.Id == controllerId)
                .Where(p => p.UserId == user.Id);
            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var sensors = _context
                .Sensors
                .Include(p => p.SensorType)
                .Where(p => p.ControllerId == controllerId);
            return Json(sensors.Adapt<SensorViewModel[]>());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = User.GetUser(_context);
            var sensor = _context.Sensors
                .Include(p => p.SensorType)
                .Where(p => p.Id == id)
                .FirstOrDefault();
            if (sensor == null) return NotFound(new NotFoundError());
            var userHasController = _context.UserHasControllers
                .Where(p => p.ControllerId == sensor.ControllerId)
                .Where(p => p.UserId == user.Id);
            if (userHasController == null) return Unauthorized(new UnauthorizedError());
            return Json(sensor.Adapt<SensorViewModel>());
        }

        [HttpPut]
        public IActionResult Put([FromBody]SensorViewModel model)
        {
            if (model == null) return StatusCode(500, new InternalServerError());
            if (!ModelState.IsValid) return StatusCode(500, new InternalServerError("Incorrect data!"));

            var user = User.GetUser(_context);

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.UserId == user.Id)
                .Where(p => p.ControllerId == model.ControllerId)
                .Where(p => p.IsAdmin);

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var sensors = _context.Sensors
                .Where(p => p.ControllerId == model.ControllerId)
                .Where(p => p.Pin == model.Pin);
            //Перевіряємо чи до цього піну підключені інші сенсори
            if (sensors.Any()) return StatusCode(500, new InternalServerError("Pin is already taken!"));
            //Додаємо сенсор
            var sensor = new Sensor
            {
                Name = model.Name,
                Pin = model.Pin,
                SensorTypeId = model.SensorTypeId,
                ControllerId = model.ControllerId,
                Value = 0,
                Status = model.Status  
            };
            _context.Sensors.Add(sensor);
            _context.SaveChanges();
            sensor.SensorType = _context.SensorTypes.Find(sensor.SensorTypeId);
            return Json(sensor.Adapt<SensorViewModel>());
        }

        [HttpPost]
        public IActionResult Post([FromBody]SensorViewModel model)
        {
            if (model == null) return StatusCode(500, new InternalServerError());
            if (!ModelState.IsValid) return StatusCode(500, new InternalServerError("Incorrect data!"));

            var sensor = _context.Sensors
                .Include(p => p.SensorType)
                .Where(p => p.Id == model.Id)
                .FirstOrDefault();

            if (sensor == null) return NotFound(new NotFoundError());

            var user = User.GetUser(_context);

            var userHasController = _context.UserHasControllers
                .Where(p => p.ControllerId == sensor.ControllerId)
                .Where(p => p.UserId == user.Id)
                .Where(p => p.IsAdmin);

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var sensors = _context.Sensors
                .Where(p => p.ControllerId == sensor.ControllerId)
                .Where(p => p.Pin == sensor.Pin)
                .Where(p => p.Id != sensor.Id);
            //Перевіряємо чи до цього піну підключені інші сенсори
            if (sensors.Any()) return StatusCode(500, new InternalServerError("Pin is already taken!"));

            //змінюємо модель
            sensor.Name = model.Name;
            sensor.Pin = model.Pin;
            sensor.Status = model.Status;
            sensor.SensorTypeId = model.SensorTypeId;

            _context.SaveChanges();
            return Json(sensor.Adapt<SensorViewModel>());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sensor = _context.Sensors
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (sensor == null) return NotFound(new NotFoundError());
            var user = User.GetUser(_context);

            var userHasController = _context.UserHasControllers
                .Where(p => p.ControllerId == sensor.ControllerId)
                .Where(p => p.UserId == user.Id);

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var scripts = _context.Scripts
                .Where(p => p.SensorId == sensor.Id)
                .Include(p => p.Commands)
                .ToArray();

            if (scripts.Any())
            {
                Command[] commands = scripts.SelectMany(p => p.Commands).ToArray();
                if (commands.Any())
                    _context.Commands.RemoveRange(commands);
                _context.Scripts.RemoveRange(scripts);
            }

            _context.Sensors.Remove(sensor);
            _context.SaveChanges();
            return NoContent();
        }
    }

}