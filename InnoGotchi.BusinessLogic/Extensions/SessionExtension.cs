using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InnoGotchi.BusinessLogic.Extensions
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            if (value is string strValue && strValue != null)
            {
                session.SetString(key, JsonConvert.SerializeObject(strValue.Replace("\"", "")));
            }
            else
            {
                session.SetString(key, JsonConvert.SerializeObject(value));
            }
        }

        public static T? Get<T>(this ISession session, string key)
        {

            var value = session.GetString(key);
            return value == null ? default :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
