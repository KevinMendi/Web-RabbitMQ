using RabbitMQ.Client;
using RabbitMQ.Helper;
using RabbitMQ.Helper.Interfaces;
using Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionProviderHelper>(
    new ConnectionProviderHelper(
    hostName: "rabbitmq",
    port: 5672,
    userName: "guest",
    password: "guest"
    ));

builder.Services.AddSingleton<ISubscriberHelper>(x => new SubscriberHelper(x.GetService<IConnectionProviderHelper>(),
    "report_exchange",
    "order_queue",
    "order.*",
    ExchangeType.Topic));

builder.Services.AddHostedService<EmailWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
