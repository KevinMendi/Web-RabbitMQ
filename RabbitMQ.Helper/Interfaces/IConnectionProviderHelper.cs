using RabbitMQ.Client;

namespace RabbitMQ.Helper.Interfaces
{
    public interface IConnectionProviderHelper: IDisposable
    {
        IConnection GetConnection();
    }
}
