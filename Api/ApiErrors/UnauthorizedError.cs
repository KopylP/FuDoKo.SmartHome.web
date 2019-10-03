using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Api.ApiErrors
{
    public class UnauthorizedError: ApiError
    {
        public UnauthorizedError(): base(401, HttpStatusCode.Unauthorized.ToString())
        {

        }

        public UnauthorizedError(string message) : base(401, HttpStatusCode.Unauthorized.ToString(), message)
        {

        }
    }
}
