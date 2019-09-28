using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core
{
    public class Communicator
    {
        
        public string Send(string message)
        {
            IConnection connection = Connect();
            IModel channel = connection.CreateModel();
            _ = channel.QueueDeclare(
                queue: "Default",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            byte[] body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "hello",
                basicProperties: null,
                body: body);
            Console.WriteLine(" [x] Sent {0}", message);
            return message;
        }

        public List<string> Receiver()
        {
            using IConnection connection = new ConnectionFactory() { HostName = "localhost" }.CreateConnection();
            using IModel channel = connection.CreateModel();
            _ = channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            List<string> messages = new List<string>();

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                string message = Encoding.UTF8.GetString(body);
                messages.Add(message);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
            return messages;
        }
    }
}