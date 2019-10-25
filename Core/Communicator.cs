using System;
using System.Text;

namespace Core
{
    public class Communicator
    {
        private CommunicatorFactory Factory { get; set; }

        public Communicator()
        {
            new InitializeApplication();
            Factory = new CommunicatorFactory();
        }

        public void Send(string message)
        {
            byte[] body = Encoding.UTF8.GetBytes(message);

            Factory.Publish("amq.direct", "", body);

            Console.WriteLine(" [x] Sent {0}", message);
        }

        public void Receiver()
        {
            RabbitMQ.Client.Events.EventingBasicConsumer consumer = CommunicatorConsumer.CreateConsumer();

            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };

            Factory.Consume(consumer);
        }
    }
}