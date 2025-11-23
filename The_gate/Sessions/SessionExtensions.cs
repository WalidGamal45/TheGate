using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace The_gate.Extensions
{
    public static class SessionExtensions
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        public static void SetObject(this ISession session, string key, object value)
        {
            if (value != null)
            {
                session.SetString(key, JsonSerializer.Serialize(value, _options));
            }
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value, _options);
        }
    }
}
