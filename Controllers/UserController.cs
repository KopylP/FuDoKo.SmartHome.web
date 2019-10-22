using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuDoKo.SmartHome.web.Api.ApiErrors;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.Data.Models;
using FuDoKo.SmartHome.web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuDoKo.SmartHome.web.Controllers
{
    /// <summary>
    /// Контроллер учета пользователей
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : BaseApiController
    {

        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="role"></param>
        /// <param name="userManager"></param>
        public UserController(ApplicationDbConrext context, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager): base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        /// <summary>
        /// Регистрация нового пользователя в системе
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody]RegisterUserViewModel model)
        {
            if (model == null) return StatusCode(500, new InternalServerError());

            if (!ModelState.IsValid) return StatusCode(500, new InternalServerError("Data is not correct!!!"));

            ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null) return BadRequest(new BadRequestError("User is already exista"));

            user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null) return BadRequest(new BadRequestError("Email is already exists"));

            var now = DateTime.Now;
            user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
                UserName = model.UserName,
                Surname = model.Surname,
                Name = model.Name,
                DisplayName = $"{model.Name} {model.Surname}",
                CreatedTime = now,
                LastModifiedDate = now
            };

            IdentityResult result =  await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(new BadRequestError(result.Errors.Select(p => p.Description).ToList()));

            await _userManager.AddToRoleAsync(user, "user");

            user.EmailConfirmed = true;
            user.LockoutEnabled = false;

            _context.SaveChanges();

            return new JsonResult(user.Adapt<UserViewModel>());
        }
    }
}
