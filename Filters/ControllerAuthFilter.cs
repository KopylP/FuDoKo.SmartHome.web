using FuDoKo.SmartHome.web.Api.ApiErrors;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Filters
{
    public class ControllerAuthFilter : Attribute, IAuthorizationFilter
    {
        private static string KEY = "fudoko-mac-address";

        ApplicationDbConrext _context;

        public ControllerAuthFilter(ApplicationDbConrext conrext)
        {
            _context = conrext;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            if (headers.ContainsKey(KEY))
            {
                headers.TryGetValue(KEY, out var macAddressValue);
                var controller = _context.Controllers
                    .Where(p => p.MAC.ToUpperInvariant().Trim() == context.HttpContext.Mac().ToUpperInvariant().Trim())
                    .FirstOrDefault();
                if(controller == null)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
