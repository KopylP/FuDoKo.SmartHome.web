using FuDoKo.SmartHome.web.Api.ApiErrors;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.Data.Models;
using FuDoKo.SmartHome.web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Controllers
{
    public class ControllerController: BaseApiController
    {
        #region constructor
        public ControllerController(ApplicationDbConrext dbContext): base(dbContext)
        {

        }
        #endregion

        #region rest methods
        /// <summary>
        /// Получить список всех контроллеров пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _conrext.Users.Find(userId);
            if (user == null) return Unauthorized(new UnauthorizedError());

            var contollers = _conrext.UserHasControllers
                .Where(p => p.UserId == user.Id)
                .Include(p => p.Controller)
                .Select(p => new { p.IsAdmin, Controller = p.Controller.Adapt<ControllerViewModel>() });
            return new JsonResult(contollers);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            //Беремо айді користувача з токену
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _conrext.Users.Find(userId);
            if (user == null) return Unauthorized(new UnauthorizedError());
            //Шукаємо контролер 
            var controller = _conrext.Controllers.Find(id);
            if (controller == null) return NotFound(new NotFoundError("Controller not found"));
            //Перевіряємо чи користувач має доступ до цього контролеру
            var userHasController = _conrext.UserHasControllers
                .Where(p => p.UserId == userId)
                .Where(p => p.ControllerId == controller.Id)
                .FirstOrDefault();
            if (userHasController == null) return Unauthorized(new UnauthorizedError());
            //Висилаємо дані про цей контролер
            return new JsonResult(controller.Adapt<ControllerViewModel>());
        }
        /// <summary>
        /// Добовление нового контроллера 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut()]
        [Authorize]
        public IActionResult Put([FromBody] ControllerViewModel model)
        {
            //Перевіряємо модель
            if (model == null) return StatusCode(500, new InternalServerError());
            if (model.MAC.Length != 16) return StatusCode(500, new InternalServerError("MAC address is incorrect"));
            var controller = _conrext.Controllers.Where(p => p.MAC == model.MAC).FirstOrDefault();
            if (controller != null) return StatusCode(500, new InternalServerError("Controller is already exists"));
            //Беремо айді користувача з токену
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _conrext.Users.Find(userId);
            if (user == null) return Unauthorized(new UnauthorizedError());
            //Створюємо контролер
            controller = model.Adapt<Data.Models.Controller>();
            controller.InstalledDate = DateTime.Now;
            controller.PublicKey = "jak to bedzie pracowac, pliz ktos wie?";
            _conrext.Controllers.Add(controller);
            //Створюємо UserHasController
            var userHasController = new UserHasController();
            userHasController.ControllerId = controller.Id;
            userHasController.UserId = userId;
            userHasController.IsAdmin = true;
            _conrext.UserHasControllers.Add(userHasController);
            //Зберігаємо зміни
            _conrext.SaveChanges();
            //Повертаємо створений контролер як резльтат
            return new JsonResult(controller.Adapt<ControllerViewModel>());
        }
        #endregion
    }
}
