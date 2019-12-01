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

            var userHasController = _context.UserHasControllers
                .Include(p => p.Controller)
                .ThenInclude(p => p.Scripts)
                .Where(p => p.UserId == user.Id && p.ControllerId == controllerId)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());
            var scripts = userHasController.Controller.Scripts.ToArray();
            return Json(scripts.Adapt<ScriptViewModel[]>());
        }
        #endregion

        #region REST methods
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var script = _context.Scripts.Find(id);

            if (script == null) return NotFound(new NotFoundError());

            var user = User.GetUser(_context);

            var userHasController = _context.UserHasControllers
                .Where(p => p.UserId == user.Id && p.ControllerId == script.ControllerId)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            return Json(script.Adapt<ScriptViewModel>());
           
        }

        [HttpPut]
        public IActionResult Put([FromBody] ScriptViewModel scriptViewModel)
        {
            if (scriptViewModel == null) return StatusCode(500, new InternalServerError());

            var user = User.GetUser(_context);

            var userHasController = _context.UserHasControllers
                .Where(p => p.UserId == user.Id && p.ControllerId == scriptViewModel.ControllerId)
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
                LastModificationDate = DateTime.Now
            };

            _context.Scripts.Add(script);
            _context.SaveChanges();

            //TODO
            return Json(script.Adapt<ScriptViewModel>());
        }

        [HttpPost]
        public IActionResult Post([FromBody] ScriptViewModel scriptViewModel)
        {
            if (scriptViewModel == null) return StatusCode(500, new InternalServerError());

            var script = _context.Scripts.Find(scriptViewModel.Id);

            if (script == null) return NotFound(new NotFoundError());

            var user = User.GetUser(_context);

            var userHasController = _context.UserHasControllers
                .Where(p => p.UserId == user.Id && p.ControllerId == scriptViewModel.ControllerId)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            script.Priority = scriptViewModel.Priority;
            script.ConditionTypeId = scriptViewModel.ConditionTypeId;
            script.RepeatTimes = scriptViewModel.RepeatTimes;
            script.TimeTo = scriptViewModel.TimeTo;
            script.TimeFrom = scriptViewModel.TimeFrom;
            script.Delta = scriptViewModel.Delta;
            script.SensorId = scriptViewModel.SensorId;
            script.Visible = true;
            script.LastModificationDate = DateTime.Now;

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

            var userHasController = _context.UserHasControllers
                .Where(p => p.UserId == user.Id && p.ControllerId == script.ControllerId)
                .FirstOrDefault();

            if (userHasController == null) return Unauthorized(new UnauthorizedError());

            _context.Scripts.Remove(script);
            _context.SaveChanges();

            return NoContent();
        }
        #endregion
    }
}
