using System.Collections.Generic;
using System.Text.Json;

namespace Registry
{
    public static class Json
    {
        public static Message Deserialize(string json)
        {
            return JsonSerializer.Deserialize<Message>(json);
        }

        public static string Serialize(Dictionary<string, List<string>> keyValuePairs)
        {
            return JsonSerializer.Serialize(keyValuePairs);
        }
    }
}
