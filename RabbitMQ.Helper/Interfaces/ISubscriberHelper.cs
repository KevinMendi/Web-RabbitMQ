namespace RabbitMQ.Helper.Interfaces
{
    public interface ISubscriberHelper : IDisposable
    {
        void Subscribe(Func<string, IDictionary<string, object>, bool> callback);
        void SubscribeAsync(Func<string, IDictionary<string, object>, Task<bool>> callback);
    }
}
