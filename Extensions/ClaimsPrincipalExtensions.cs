using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static ApplicationUser GetUser(this ClaimsPrincipal User, ApplicationDbConrext _context)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _context.Users.Find(userId);
        }
    }
}
