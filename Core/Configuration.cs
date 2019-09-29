using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Specialized;
namespace Core
{
    public sealed class Configuration
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
