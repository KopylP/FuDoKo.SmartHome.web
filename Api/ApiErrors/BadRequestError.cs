using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Api.ApiErrors
{
    public class BadRequestError: ApiError
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> Errors { get; set; }

        public BadRequestError() : base(400, HttpStatusCode.BadRequest.ToString()) { }

        public BadRequestError(string message) : base(400, HttpStatusCode.BadRequest.ToString(), message) { }

        public BadRequestError(List<string> errors) : base(400, HttpStatusCode.BadRequest.ToString())
        {
            Errors = errors;
        }
    }
}
