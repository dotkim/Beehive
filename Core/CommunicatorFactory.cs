using RabbitMQ.Client;
using System.Collections.Generic;

namespace Core
{
    public class CommunicatorFactory
    {
        private Configuration Config { get; set; }
        private IConnection Connection { get; set; }
        private IModel Channel { get; set; }

        public CommunicatorFactory()
        {
            Connection = Connect();
            Channel = CreateModel();
        }

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

        private IConnection Connect()
        {
            ConnectionFactory factory = ConnectionConfiguration();
            return factory.CreateConnection();
        }

        public IModel CreateModel()
        {
            return Connection.CreateModel();
        }

        public void DeclareQueue(
            string que,
            bool dur = false,
            bool excl = false,
            bool del = false,
            IDictionary<string, object> args = null)
        {
            _ = Channel.QueueDeclare(
                queue: que,
                durable: dur,
                exclusive: excl,
                autoDelete: del,
                arguments: args);
        }

        public void Publish(
            string exc,
            string key,
            byte[] body,
            IBasicProperties prop = null)
        {
            Channel.BasicPublish(
                exchange: exc,
                routingKey: key,
                body: body,
                basicProperties: prop);
        }
    }
}
