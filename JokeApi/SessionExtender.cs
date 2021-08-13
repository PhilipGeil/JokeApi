using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeApi
{
    public static class SessionExtender
    {
        /// <summary>
        /// Method for serializing an object and the storing it the session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObjectAsJson(this ISession session, string key,
object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// Gets the session and desrializes it to an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
           JsonConvert.DeserializeObject<T>(value);
        }
    }
}
