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
    public class CategoryController : ControllerBase
    {
        readonly DalManager dal = new();

        /// <summary>
        /// Gets all of the categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<string> Get()
        {
            return dal.GetAllCategories(Validator.GetLanguageHeader(Request.Headers));
        }
    }
}
