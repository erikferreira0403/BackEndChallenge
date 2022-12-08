using DesafioFinal.Models;
using DesafioFinal.Repositorio.MessageRepo;
using DesafioFinal.Repositorio.SubscriptionRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesafioFinal.Controllers
{
    [Route("api/messages")]
    public class MessageController : Controller
    {
        private readonly ISubscriptionRepo _repositorio;
        private readonly IMessageConfiguration _configuration;
        public MessageController(IMessageConfiguration configuration, ISubscriptionRepo repo)
        {
            _configuration = configuration;
            _repositorio = repo;
        }

        [HttpPost("Enviar")]
        public IActionResult SendMessage([FromBody] User SendMessageInputModel)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            factory.UserName = "guest";
            factory.Password = "guest";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "CriarUser", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var stringMessage =
                 JsonSerializer.Serialize(SendMessageInputModel);
                    var byteArray = Encoding.UTF8.GetBytes(stringMessage);

                    channel.BasicPublish(exchange: "", routingKey: "CriarUser", basicProperties: null, body: byteArray);
                }
            }
            return Ok();
        }

        [HttpPut("Desativar/{id}")]
        public IActionResult DesativarSubscription(int id, [FromBody] Status status)
        {
            status.Id = id;

            var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            factory.UserName = "guest";
            factory.Password = "guest";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "DesativarUser", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var stringMessage =
                 JsonSerializer.Serialize(status);
                    var byteArray = Encoding.UTF8.GetBytes(stringMessage);

                    channel.BasicPublish(exchange: "", routingKey: "DesativarUser", basicProperties: null, body: byteArray);
                }
            }
            return Ok();
        }

        [HttpPut("Reativar/{id}")]
        public IActionResult ReativarSubscription(int id, [FromBody] Status status)
        {
            status.Id = id;

            var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            factory.UserName = "guest";
            factory.Password = "guest";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "ReativarUser", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var stringMessage =
                 JsonSerializer.Serialize(status);
                    var byteArray = Encoding.UTF8.GetBytes(stringMessage);

                    channel.BasicPublish(exchange: "", routingKey: "ReativarUser", basicProperties: null, body: byteArray);
                }
            }
            return Ok();
        }

        [HttpPost("iniciar Consumer")]
        public IActionResult StartConsumer ()
        {
            _configuration.IniciarFilas();
            return Accepted();
        }
        [HttpPut("iniciar Consumer Desativar")]
        public IActionResult StartConsumerDesativar()
        {
            _configuration.IniciarFilaDesativar();
            return Accepted();
        }

        [HttpPut("iniciar Consumer Reativar")]
        public IActionResult StartConsumerReativar()
        {
            _configuration.IniciarFilaReativar();
            return Accepted();
        }

    }
}
