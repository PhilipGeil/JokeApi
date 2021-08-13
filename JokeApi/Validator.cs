using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace JokeApi
{
    public class Validator
    {
        /// <summary>
        /// Validation method for the Accept-Language header
        /// Checks if it is present and then veryfies that is either <b>DA</b> or <b>EN</b> <i>(The two supported options)</i><br></br>
        /// Otherwise it returns EN
        /// </summary>
        /// <param name="header"></param>
        /// <returns>Chosen language or else EN</returns>
        public static string GetLanguageHeader(IHeaderDictionary header)
        {
            if (header["Accept-Language"].Count > 0)
            {
                string h = header["Accept-Language"][0];
                if (h == "DA" || h == "da" || h == "dk" || h == "DA-dk")
                {
                    return "DA";
                }
            }
            return "EN";
        }

        private static List<string> level1API = new List<string>()
        {
            "test",
        };
        private static List<string> level2API = new List<string>()
        {
            "test1",
        };
        private static List<string> level3API = new List<string>()
        {
            "test2",
        };

        /// <summary>
        /// Authenticate method, checks if provided API Key is included if any of the 3 permissions lists - and checks with the provided category ID 
        /// if the key holds the right permissions.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static bool Authenticate(string key, int category)
        {
            if (level1API.Contains(key) && category == 0)
            {
                return true;
            }
            else if (level2API.Contains(key) && new List<int>() { 0, 1, 2 }.Contains(category))
            {
                return true;
            }
            else if (level3API.Contains(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Generates a new API Key and adds it to the desired list
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public static string GenerateApiKey(string permissions)
        {
            byte[] key = new byte[32];
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);
            if (permissions == "level1")
            {
                level1API.Add(apiKey);
                return apiKey;
            } else if (permissions == "level2") {
                level2API.Add(apiKey);
                return apiKey;
            } else if (permissions == "level3")
            {
                level3API.Add(apiKey);
                return apiKey;
            } else
            {
                return "error";
            }
        }
    }
}
