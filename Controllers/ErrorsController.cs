using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FuDoKo.SmartHome.web.Api.ApiErrors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuDoKo.SmartHome.web.Controllers
{
    [Route("/errors")]
    public class ErrorsController : Controller
    {
        [HttpGet("{code}")]
        public IActionResult Error(int code)
        {
            HttpStatusCode statusCode = (HttpStatusCode)code;
            return new JsonResult(new ApiError(code, statusCode.ToString()));
        }
    }
}
