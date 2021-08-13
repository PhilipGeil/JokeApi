using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using http = System.Web.Http;

namespace JokeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokeController : ControllerBase
    {
        DalManager dal = new DalManager();
        /// <summary>
        /// Gets a random joke, from the requested or saved category.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Joke> RandomJoke(string key, int categoryID)
        {

            int c = GetCategoryId(categoryID); 


            bool roles = Validator.Authenticate(key, c);
            if (!roles)
            {
                return Unauthorized();
            }

            // Validate the chosen language - checks if it is DA otherwise it sets it to EN
            string lang = Validator.GetLanguageHeader(Request.Headers);

            List<int> excluded = GetExcludedList();

            Joke joke = dal.GetRandomJoke(c, excluded, lang);
            // If joke is not null - it was succesfull in getting a random joke - else there was no more jokes
            if (joke != null)
            {
                excluded.Add(joke.Id);
                HttpContext.Session.SetObjectAsJson("ExcludedJokes", excluded);
                return joke;
            }

            // Check the prefered language - returns same error message.
            if (lang == "DA")
            {
                return Content("Der er ikke flere jokes i denne kategori. \nDu kan enten prøve en anden kategori eller rydde din session.");
            }
            return Content("There are no more jokes in this category. \nYou can either try another category or clear your session.");
        }

        private int GetCategoryId(int categoryID)
        {
            int c = 0;

            if (!Request.QueryString.Value.Contains("categoryid"))
            {
                if (Request.Cookies["favoriteCategory"] != null)
                {
                    try
                    {
                        c = int.Parse(Request.Cookies["favoriteCategory"]);

                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("There was an error parsing the category id");
                    }
                }
            }
            else
            {
                c = categoryID;
                Response.Cookies.Append("favoriteCategory", c.ToString());
            }
            return c;
        }

        /// <summary>
        /// Gets the list of excluded jokes from the session or creates an empty list if no session is present.
        /// </summary>
        /// <returns>A list of integers with the excluded Joke.Id</returns>
        private List<int> GetExcludedList()
        {

            List<int> excluded;
            //Check if there already is a session
            if (CheckForSession("ExcludedJokes"))
            {
                excluded = HttpContext.Session.GetObjectFromJson<List<int>>("ExcludedJokes");
            }
            else
            {
                excluded = new List<int>();
            }
            return excluded;
        }

        /// <summary>
        /// Checks if there is a session allready
        /// </summary>
        /// <param name="sessionName"></param>
        /// <returns></returns>
        private bool CheckForSession(string sessionName)
        {
            return HttpContext.Session.GetString(sessionName) != null;
        }
    }
}
