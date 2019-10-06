using FuDoKo.SmartHome.web.Api.ApiErrors;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.Data.Models;
using FuDoKo.SmartHome.web.Extensions;
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
    [Authorize]
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
        public IActionResult Get()
        {
            var user = User.GetUser(_context);
            if (user == null) return Unauthorized(new UnauthorizedError());

            var contollers = _context.UserHasControllers
                .Where(p => p.UserId == user.Id)
                .Include(p => p.Controller)
                .Select(p => new { p.IsAdmin, Controller = p.Controller.Adapt<ControllerViewModel>() });
            return new JsonResult(contollers);
        }
        /// <summary>
        /// Получаем контроллер по айди
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //Беремо айді користувача з токену
            var user = User.GetUser(_context);
            if (user == null) return Unauthorized(new UnauthorizedError());
            //Шукаємо контролер 
            var controller = _context.Controllers.Find(id);
            if (controller == null) return NotFound(new NotFoundError("Controller not found"));
            //Перевіряємо чи користувач має доступ до цього контролеру
            var userHasController = _context.UserHasControllers
                .Where(p => p.UserId == user.Id)
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
        public IActionResult Put([FromBody] ControllerViewModel model)
        {
            //Перевіряємо модель
            if (model == null) return StatusCode(500, new InternalServerError());
            if (model.MAC.Length != 16) return StatusCode(500, new InternalServerError("MAC address is incorrect"));
            var controller = _context.Controllers.Where(p => p.MAC == model.MAC).FirstOrDefault();
            if (controller != null) return StatusCode(500, new InternalServerError("Controller is already exists"));
            //Беремо айді користувача з токену
            var user = User.GetUser(_context);
            if (user == null) return Unauthorized(new UnauthorizedError());
            //Створюємо контролер
            controller = model.Adapt<Data.Models.Controller>();
            controller.InstalledDate = DateTime.Now;
            controller.PublicKey = "jak to bedzie pracowac, pliz ktos wie?";
            _context.Controllers.Add(controller);
            //Створюємо UserHasController
            var userHasController = new UserHasController();
            userHasController.ControllerId = controller.Id;
            userHasController.UserId = user.Id;
            userHasController.IsAdmin = true;
            _context.UserHasControllers.Add(userHasController);
            //Зберігаємо зміни
            _context.SaveChanges();
            //Повертаємо створений контролер як резльтат
            return new JsonResult(controller.Adapt<ControllerViewModel>());
        }

        /// <summary>
        /// Изменение контроллера.
        /// Меняем Name, MAC, Status.
        /// Status на 0 желательно менять методом delete.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] ControllerViewModel model)
        {
            //Беремо айді користувача з токену
            var user = User.GetUser(_context);
            if (user == null) return Unauthorized(new UnauthorizedError());
            //Перевіряємо модель
            if (model == null && !ModelState.IsValid) return StatusCode(500, new InternalServerError());
            var controller = _context.Controllers
                .Where(p => p.MAC == model.MAC && p.Id != model.Id)
                .FirstOrDefault();
            if(controller != null) return StatusCode(500, new InternalServerError("Controller is ulready exists"));
            controller = _context.Controllers.Find(model.Id);
            if (controller == null) return NotFound(new NotFoundError());
            //Змінюємо контролер
            controller.MAC = model.MAC;
            controller.Name = model.Name;
            controller.Status = model.Status;
            //Зберігаємо зміни
            _context.Controllers.Update(controller);
            _context.SaveChanges();
            //Повертаємо результат
            return new JsonResult(controller.Adapt<ControllerViewModel>());
        }

        #endregion
    }
}
