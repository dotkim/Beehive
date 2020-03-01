using System.Collections.Generic;

namespace Registry
{
    public class MessageContent
    {
        public Dictionary<string, List<string>> RoutingKeys { get; set; }
        public string Queue { get; set; }
        public string ApplicationName { get; set; }
    }

    public class Message
    {
        public string Action { get; set; }
        public MessageContent Content { get; set; }
    }
}
