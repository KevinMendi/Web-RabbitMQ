namespace RabbitMQ.Helper.Interfaces
{
    public interface IPublisherHelper : IDisposable
    {
        void Publish(string message, string routingKey, IDictionary<string, object> messageAttributes, string timeToLive = null);
    }
}
