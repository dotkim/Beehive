using RabbitMQ.Client.Events;

namespace Core
{
    /// <summary>
    /// Communicator consumer class
    /// </summary>
    class CommunicatorConsumer
    {
        /// <summary>
        /// Creates a new consumer.
        /// </summary>
        /// <returns>consumer of typeEventingBasicConsumer</returns>
        public static EventingBasicConsumer CreateConsumer()
        {
            return new EventingBasicConsumer(CommunicatorFactory.GetChannel());
        }
    }
}
