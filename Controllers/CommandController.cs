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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuDoKo.SmartHome.web.Controllers
{
    public class CommandController : BaseApiController
    {
        #region constructor
        public CommandController(ApplicationDbConrext context) : base(context)
        {
        }
        #endregion

        #region methods
        [HttpGet("All/{scriptId}")]
        public IActionResult All(int scriptId)
        {
            var user = User.GetUser(_context);

            var script = _context.Scripts.Find(scriptId);

            if (script == null) return NotFound(new NotFoundError());

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.ControllerId == script.ControllerId)
                .Where(p => p.UserId == user.Id)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var commands = _context.Commands.Where(p => p.ScriptId == script.Id).ToArray();

            return Json(commands.Adapt<CommandViewModel[]>());
        }
        #endregion

        #region REST methods
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = User.GetUser(_context);

            var command = _context
                .Commands
                .Include(p => p.DeviceConfiguration)
                .ThenInclude(p => p.Device)
                .Include(p => p.DeviceConfiguration)
                .ThenInclude(p => p.Measure)
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (command == null) return NotFound(new NotFoundError());

            var script = _context.Scripts.Find(command.ScriptId);

            if (script == null) return Unauthorized(new UnauthorizedError());

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.ControllerId == script.ControllerId)
                .Where(p => p.UserId == user.Id)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            return Json(command.Adapt<CommandViewModel>());
        }

        [HttpPut]
        public IActionResult Put([FromBody] CommandViewModel model)
        {
            if (model == null) return StatusCode(500, new InternalServerError());
            var user = User.GetUser(_context);
            if (user == null) return Unauthorized(new UnauthorizedError());
            var script = _context.Scripts.Find(model.ScriptId);
            if (script == null) return NotFound(new NotFoundError("Script not found."));

            var userHasController = _context
                .UserHasControllers
                 .Where(p => p.ControllerId == script.ControllerId)
                .Where(p => p.UserId == user.Id)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var command = new Command
            {
                Name = model.Name,
                ScriptId = model.ScriptId,
                End = model.End,
                DeviceConfigurationId = model.DeviceConfigurationId,
                TimeSpan = model.TimeSpan
            };

            _context.Commands.Add(command);
            _context.SaveChanges();

            try
            {
                command.DeviceConfiguration = _context.DeviceConfigurations.Find(command.DeviceConfigurationId);
                command.DeviceConfiguration.Device = _context.Devices.Find(command.DeviceConfiguration.DeviceId);
                command.DeviceConfiguration.Measure = _context.Measures.Find(command.DeviceConfiguration.MeasureId);
            }
            catch (NullReferenceException exeption) { }

            return Json(command.Adapt<CommandViewModel>());
        }

        [HttpPost]
        public IActionResult Post([FromBody] CommandViewModel model)
        {
            var user = User.GetUser(_context);

            var command = _context.Commands.Find(model.Id);
            if (command == null) return NotFound(new NotFoundError("Command not found."));

            var script = _context.Scripts.Find(model.ScriptId);
            if (script == null) return NotFound(new NotFoundError("Script not found."));

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.ControllerId == script.ControllerId)
                .Where(p => p.UserId == user.Id)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            command.Name = model.Name;
            command.TimeSpan = model.TimeSpan;

            _context.Commands.Update(command);
            _context.SaveChanges();

            try
            {
                command.DeviceConfiguration = _context.DeviceConfigurations.Find(command.DeviceConfigurationId);
                command.DeviceConfiguration.Device = _context.Devices.Find(command.DeviceConfiguration.DeviceId);
                command.DeviceConfiguration.Measure = _context.Measures.Find(command.DeviceConfiguration.MeasureId);
            }
            catch (NullReferenceException exeption) { }

            return Json(command.Adapt<CommandViewModel>());

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var user = User.GetUser(_context);

            var command = _context.Commands.Find(id);
            if (command == null) return NotFound(new NotFoundError("Command not found."));

            var script = _context.Scripts.Find(command.ScriptId);
            if (script == null) return NotFound(new NotFoundError("Script not found."));

            var userHasController = _context
                .UserHasControllers
                .Where(p => p.ControllerId == script.ControllerId)
                .Where(p => p.UserId == user.Id)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            _context.Commands.Remove(command);
            _context.SaveChanges();

            return NoContent();
        }
        #endregion
    }
}