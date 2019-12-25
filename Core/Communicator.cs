using System;
using System.Text;

namespace Core
{
    /// <summary>
    /// The rabbitmq communicator class.
    /// contains methods for communicating with rabbitmq. Uses the CommunicatorConsumer and CommunicatorFactory classes.
    /// </summary>
    public class Communicator
    {
        /// <value>Gets the instance of the CommunicatorFactory class.</value>
        private CommunicatorFactory Factory { get; }

        /// <summary>
        /// Creates a new instance of the Communicator class, takes no parameters.
        /// </summary>
        /// <example>var communicator = new Communicator();</example>
        public Communicator()
        {
            new InitializeApplication();
            Factory = new CommunicatorFactory();
        }

        /// <summary>
        /// Sends a new message to the applications queue.
        /// </summary>
        /// <param name="message">The string message to send, should be JSON to be parsed unless using a different parser.</param>
        public void Send(string message)
        {
            byte[] body = Encoding.UTF8.GetBytes(message);

            Factory.Publish("amq.direct", "", body);

            Console.WriteLine(" [x] Sent {0}", message);
        }

        /// <summary>
        /// The rabbitmq queue listener.
        /// This method listens for new messages from the subscribed queue.
        /// </summary>
        public void Receiver(Action<string> callback)
        {
            RabbitMQ.Client.Events.EventingBasicConsumer consumer = CommunicatorConsumer.CreateConsumer();

            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                callback.Invoke(message);
            };

            Factory.Consume(consumer);
        }
    }
}