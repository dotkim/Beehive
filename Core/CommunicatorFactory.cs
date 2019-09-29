using RabbitMQ.Client;

namespace Core
{
    class CommunicatorFactory
    {
        public ConnectionFactory ConnectionConfiguration()
        {
            Configuration config = GetConfiguration();
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
