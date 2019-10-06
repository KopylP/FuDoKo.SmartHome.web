using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuDoKo.SmartHome.web.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuDoKo.SmartHome.web.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        #region fields
        protected ApplicationDbConrext _context;
        protected readonly Newtonsoft.Json.JsonSerializerSettings _jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.Indented
        };
        #endregion

        #region constructor
        public BaseApiController(ApplicationDbConrext context)
        {
            _context = context;
        }
        #endregion
    }
}
