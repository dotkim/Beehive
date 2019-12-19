using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// Configuration class.
    /// Sets the default and/or provided configuration settings.
    /// </summary>
    public class Configuration
    {
        /// <value>The RabbitMQ host URI.</value>
        public string RmqUri { get; set; } = "localhost";

        /// <value>RabbitMQ Port.</value>
        public int RmqPort { get; set; } = 5672;

        /// <value>RabbitMQ username.</value>
        public string RmqUserName { get; set; } = "guest";

        /// <value>RabbitMQ password.</value>
        public string RmqPassword { get; set; } = "guest";

        /// <value>The applications RabbitMQ queue.</value>
        public string RmqQueue { get; set; } = "Default";

        /// <value>Application name, should always be set.</value>
        public string ApplicationName { get; set; } = "DefaultApplication";

        /// <value>A dictionary of subscribed exchanges and fields, this should be all routing keys provided by the registry</value>
        public Dictionary<string, string> RoutingKeys { get; set; } = new Dictionary<string, string>();
    }
}