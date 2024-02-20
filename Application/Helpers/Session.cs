using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicSystem.Core.Application.Helpers
{
    public static class Session
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value == null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
