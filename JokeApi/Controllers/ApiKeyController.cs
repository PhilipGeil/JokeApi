using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiKeyController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get(string permissions)
        {
            return Validator.GenerateApiKey(permissions);
        }
    }
}
