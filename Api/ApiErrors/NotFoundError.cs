using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace FuDoKo.SmartHome.web.Api.ApiErrors
{
    public class NotFoundError : ApiError
    {
        public NotFoundError() : base(404, HttpStatusCode.NotFound.ToString())
        {
        }

        public NotFoundError(string message) : base(400, HttpStatusCode.NotFound.ToString(), message)
        {
        }
    }
}
