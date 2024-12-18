using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Belan_30323.Domain
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T item)
        {
            var serializedItem = JsonSerializer.Serialize(item);
            session.SetString(key, serializedItem);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var item = session.GetString(key);
            return item == null
                ? Activator.CreateInstance<T>()
                : JsonSerializer.Deserialize<T>(item);
        }
    }
}
