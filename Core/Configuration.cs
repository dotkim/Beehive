using System.Collections.Generic;

namespace Core
{
    public class Configuration
    {
        public string RmqUri { get; set; } = "localhost";
        public int RmqPort { get; set; } = 5672;
        public string RmqUserName { get; set; } = "guest";
        public string RmqPassword { get; set; } = "guest";
        public string RmqQueue { get; set; } = "Default";

        public string ApplicationName { get; set; } = "DefaultApplication";

        public List<string> SubscribedFields { get; set; }
        public List<string> SubscribedExchanges { get; set; }
    }
}