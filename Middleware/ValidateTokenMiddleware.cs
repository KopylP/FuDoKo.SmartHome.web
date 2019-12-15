using FuDoKo.SmartHome.web.Api.ApiErrors;
using FuDoKo.SmartHome.web.Data;
using FuDoKo.SmartHome.web.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Middleware
{
    public class ValidateTokenMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationDbConrext _conrext;

        public ValidateTokenMiddleware(ApplicationDbConrext context)
        {
            this._conrext = context;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value == null)
            {
                await next.Invoke(context).ConfigureAwait(false);
            }
            else
            {
                var user = context?.User?.GetUser(_conrext);
                if (user == null)
                {
                    context.Response.StatusCode = 401;
                    string error = JsonConvert.SerializeObject(new UnauthorizedError());
                    await context.Response.WriteAsync(error).ConfigureAwait(false);
                }
                else
                {
                    await next.Invoke(context).ConfigureAwait(false);
                }
            }
        }
    }
}
