using RabbitMQ.Client;

namespace Core
{
    public class CommunicatorFactory
    {
        private Configuration config { get; set; }
        private readonly IConfiguration configuration;
        private ConnectionFactory ConnectionConfiguration()
        {
            config = configuration.GetConfiguration();
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = config.RmqUserName,
                Password = config.RmqPassword,
                HostName = config.RmqUri,
                Port = config.RmqPort
            };
            return factory;
        }

        public IConnection Connect()
        {
            ConnectionFactory factory = ConnectionConfiguration();
            return factory.CreateConnection();
        }
    }
}
