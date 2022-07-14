using RabbitMQ.Client.Exceptions;
using RabbitMQ.Helper.Interfaces;

namespace Worker
{
    public class EmailWorker : IHostedService
    {
        private readonly ISubscriberHelper _subscriber;
        public EmailWorker(ISubscriberHelper subscriber)
        {
            _subscriber = subscriber;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _subscriber.Subscribe(ProcessMessage);
                Console.WriteLine($" [x] Received from Rabbit");

                // simulate an async email process
                await Task.Delay(new Random().Next(1, 3) * 1000, cancellationToken);

                Console.WriteLine($"Sending order confirmation email");

                Task.Delay(10000).Wait();
            }
            catch (AlreadyClosedException)
            {
               Console.WriteLine("RabbitMQ is closed!");
            }
            catch (Exception e)
            {
                Console.WriteLine(default, e, e.Message);
            }

            await Task.CompletedTask;
        }

        private bool ProcessMessage(string message, IDictionary<string, object> headers)
        {
            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
