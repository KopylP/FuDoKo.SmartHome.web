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
    public class ScriptController : BaseApiController
    {
        #region constructor
        public ScriptController(ApplicationDbConrext context) : base(context)
        {
        }
        #endregion

        #region methods
        [HttpGet("All/{controllerId}")]
        public IActionResult All(int controllerId)
        {
            var user = User.GetUser(_context);

            var scripts = _context
                .Scripts
                .Where(p => !p.Complited)
                .Where(p => p.ControllerId == controllerId)
                .Where(p => p.UserId == user.Id)
                .ToArray();
            return Json(scripts.Adapt<ScriptViewModel[]>());
        }
        #endregion

        #region REST methods
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = User.GetUser(_context);

            var script = _context.Scripts
                .Include(p => p.ConditionType)
                .Include(p => p.Sensor)
                .Where(p => p.Id == id)
                .Where(p => p.UserId == user.Id)
                .FirstOrDefault();

            if (script == null) return NotFound(new NotFoundError());

            return Json(script.Adapt<ScriptViewModel>());
           
        }

        [HttpPut]
        public IActionResult Put([FromBody] ScriptViewModel scriptViewModel)
        {
            if (scriptViewModel == null) return StatusCode(500, new InternalServerError());

            var user = User.GetUser(_context);

            var userHasController = _context.UserHasControllers
                .Where(p => p.UserId == user.Id)
                .Where(p => p.ControllerId == scriptViewModel.ControllerId)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            var script = new Script
            {
                ControllerId = scriptViewModel.ControllerId,
                Priority = scriptViewModel.Priority,
                ConditionTypeId = scriptViewModel.ConditionTypeId,
                Complited = false,
                RepeatTimes = scriptViewModel.RepeatTimes,
                TimeTo = scriptViewModel.TimeTo,
                TimeFrom = scriptViewModel.TimeFrom,
                Delta = scriptViewModel.Delta,
                SensorId = scriptViewModel.SensorId,
                Visible = true,
                LastModificationDate = DateTime.Now,
                ConditionValue = scriptViewModel.ConditionValue,
                UserId = user.Id,
                Status = false, 
                Name = scriptViewModel.Name
            };

            _context.Scripts.Add(script);
            _context.SaveChanges();

            return Json(script.Adapt<ScriptViewModel>());
        }

        [HttpPost]
        public IActionResult Post([FromBody] ScriptViewModel scriptViewModel)
        {
            if (scriptViewModel == null) return StatusCode(500, new InternalServerError());

            var script = _context.Scripts.Find(scriptViewModel.Id);

            if (script == null) return NotFound(new NotFoundError());

            var user = User.GetUser(_context);

            if (script.UserId != user.Id) return Unauthorized(new UnauthorizedError());

            script.Priority = scriptViewModel.Priority;
            script.ConditionTypeId = scriptViewModel.ConditionTypeId;
            script.RepeatTimes = scriptViewModel.RepeatTimes;
            script.TimeTo = scriptViewModel.TimeTo;
            script.TimeFrom = scriptViewModel.TimeFrom;
            script.Delta = scriptViewModel.Delta;
            script.SensorId = scriptViewModel.SensorId;
            script.Visible = true;
            script.LastModificationDate = DateTime.Now;
            script.Status = scriptViewModel.Status;
            script.Name = scriptViewModel.Name;
            _context.Scripts.Update(script);
            _context.SaveChanges();

            //TODO
            return Json(script.Adapt<ScriptViewModel>());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var script = _context.Scripts.Find(id);

            if (script == null) return NotFound(new NotFoundError());

            var user = User.GetUser(_context);

            if(script.UserId != user.Id)
            {
                return Unauthorized(new UnauthorizedError());
            }

            script.Complited = true;
            _context.Scripts.Update(script);
            _context.SaveChanges();

            return NoContent();
        }
        #endregion
    }
}
