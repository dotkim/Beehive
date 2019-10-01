using RabbitMQ.Client;

namespace Core
{
    public class CommunicatorFactory
    {
        private Configuration Config { get; set; }

        private ConnectionFactory ConnectionConfiguration()
        {
            Config = InitializeApplication.GetConfiguration();
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = Config.RmqUserName,
                Password = Config.RmqPassword,
                HostName = Config.RmqUri,
                Port = Config.RmqPort
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
