using RabbitMQ.Client;
using RabbitMQ.Helper;
using RabbitMQ.Helper.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IConnectionProviderHelper>(
    new ConnectionProviderHelper(
    hostName: "rabbitmq",
    port: 5672,
    userName: "guest",
    password: "guest"
    ));

builder.Services.AddScoped<IPublisherHelper>(x => new PublisherHelper(x.GetService<IConnectionProviderHelper>(),
    "report_exchange",
    ExchangeType.Topic));

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
