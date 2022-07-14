using RabbitMQ.Client;
using RabbitMQ.Helper.Interfaces;

namespace RabbitMQ.Helper
{
    public class ConnectionProviderHelper : IConnectionProviderHelper
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private bool _disposed;

        public ConnectionProviderHelper(string hostName, int port, string userName, string password)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = hostName ?? "localhost",
                Port = port,
                UserName = userName ?? "guest",
                Password = password ?? "guest"
            };

            _connection = _connectionFactory.CreateConnection();
        }

        public IConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _connection?.Close();

            _disposed = true;
        }
    }
}
