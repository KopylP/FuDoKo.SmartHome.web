using FuDoKo.SmartHome.web.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Extensions
{
    public static class ValidateTokenMiddlewareExtension
    {
        public static IApplicationBuilder UseValidateTocken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateTokenMiddleware>();
        }
    }
}
