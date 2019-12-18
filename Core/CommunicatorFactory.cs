using RabbitMQ.Client;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// The Communicator factory class.
    /// This class contains methods which is used by the communicator class.
    /// The main objective is to create connections, channels, exchanges and queues in this class.
    /// </summary>
    public class CommunicatorFactory
    {
        /// <value>Gets or sets the Config class instance generated on init.</value>
        private Configuration Config { get; set; }

        /// <value>Gets or sets the connection to RabbitMQ</value>
        private IConnection Connection { get; set; }

        /// <value>Gets or sets the connection channel</value>
        private static IModel Channel { get; set; }

        /// <summary>
        /// The class constructor.
        /// Creates a new instance of the CommunicatorFactory class.
        /// Upon call, the connection, channel and default queue is declared (if it does not exist).
        /// </summary>
        public CommunicatorFactory()
        {
            Connection = Connect();
            Channel = CreateModel();
            DeclareQueue(Config.RmqQueue);
        }

        /// <summary>
        /// Gets the connection channel.
        /// This is to provide the channel to other helper classes.
        /// </summary>
        /// <returns>Channel of type IModel</returns>
        public static IModel GetChannel()
        {
            return Channel;
        }

        /// <summary>
        /// Creates a new instance of the ConnectionFactory from the rabbitmq lib.
        /// Uses the config connection values to connect to the rmq host.
        /// </summary>
        /// <returns>Factory object of type ConnectionFactory.</returns>
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

        /// <summary>
        /// Connects to the rmq host.
        /// </summary>
        /// <returns>Connection of type IConnection.</returns>
        private IConnection Connect()
        {
            ConnectionFactory factory = ConnectionConfiguration();
            return factory.CreateConnection();
        }

        /// <summary>
        /// Creates a model (channel).
        /// 
        /// </summary>
        /// <returns>model of type IModel</returns>
        public IModel CreateModel()
        {
            return Connection.CreateModel();
        }

        /// <summary>
        /// Declares a queue.
        /// 
        /// </summary>
        /// <param name="que"></param>
        /// <param name="dur"></param>
        /// <param name="excl"></param>
        /// <param name="del"></param>
        /// <param name="args"></param>
        public void DeclareQueue(
            string que,
            bool dur = false,
            bool excl = false,
            bool del = false,
            IDictionary<string, object> args = null)
        {
            Channel.QueueDeclareNoWait(
                queue: que,
                durable: dur,
                exclusive: excl,
                autoDelete: del,
                arguments: args);
        }

        /// <summary>
        /// Publishes a new message to the exchange.
        /// </summary>
        /// <param name="exc">The exchange to publish to.</param>
        /// <param name="key">The routing key for the message, this should be a property of the exchange</param>
        /// <param name="body">The message body</param>
        /// <param name="prop">Extra properties, these are of IBasicProperties (Optional)</param>
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

        /// <summary>
        /// Consume a message from a queue.
        /// Gets the message event and acks(optional) the message.
        /// </summary>
        /// <param name="consmr">Consumer for capturing the event.</param>
        /// <param name="ack">Set to false if the message shouldn't be acked (Optional)</param>
        /// <param name="que">The queue to consume messages from.(Optional)</param>
        public void Consume(IBasicConsumer consmr, bool ack = true, string que = "Default")
        {
            Channel.BasicConsume(queue: que, autoAck: ack, consumer: consmr);
        }
    }
}
