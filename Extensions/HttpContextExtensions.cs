using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Extensions
{
    public static class HttpContextExtensions
    {
        public static string Mac(this HttpContext context)
        {
            context.Request.Headers.TryGetValue("fudoko-mac-address", out var macAddressValue);
            var macAddress = macAddressValue.FirstOrDefault();
            return macAddress;
        }
    }
}
