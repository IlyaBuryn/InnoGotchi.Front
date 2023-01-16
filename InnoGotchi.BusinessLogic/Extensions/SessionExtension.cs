using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InnoGotchi.BusinessLogic.Extensions
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            if (value is string && value != null)
                session.SetString(key, JsonConvert.SerializeObject((value as string).Replace("\"", "")));
            else
                session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {

            var value = session.GetString(key);
            return value == null ? default :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
