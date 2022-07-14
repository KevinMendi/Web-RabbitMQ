using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Helper.Interfaces;
using Resto.Common.Models;
using System.Text.Json;

namespace RestoApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        //private readonly IOrderService _orderService;
        private readonly IPublisherHelper _publisher;
        private static readonly List<Order> Orders = new List<Order>();

        public OrderController(IPublisherHelper publisher)
        {
            //_orderService = orderService;
            _publisher = publisher;
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var order = Orders.SingleOrDefault(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Post(Order request)
        {
            var newOrder = new Order
            {
                Id = Orders.Select(x => x.Id).DefaultIfEmpty().Max() + 1,
                Email = request.Email
            };

            Orders.Add(newOrder);
            var payload = JsonSerializer.Serialize(newOrder);
            Console.WriteLine($"New order created: {payload}");

            //_orderService.Publish("", "order", payload);
            _publisher.Publish(payload, "order.route", null);

            return CreatedAtAction(nameof(GetById), new { id = newOrder.Id }, newOrder);
        }
    }
}
