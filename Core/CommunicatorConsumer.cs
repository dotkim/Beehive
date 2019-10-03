using RabbitMQ.Client.Events;

namespace Core
{
    class CommunicatorConsumer
    {
        public static EventingBasicConsumer CreateConsumer()
        {
            return new EventingBasicConsumer(CommunicatorFactory.GetChannel());
        }
    }
}
