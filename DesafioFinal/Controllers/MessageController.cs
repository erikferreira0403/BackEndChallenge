using DesafioFinal.Models;
using DesafioFinal.Repositorio.MessageRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DesafioFinal.Controllers
{
    [Route("api/messages")]
    public class MessageController : Controller
    {
        private readonly IMessageConfiguration _configuration;
        public MessageController(IMessageConfiguration configuration)
        {
           
            _configuration = configuration;
        }
        [HttpPost("Enviar")]
        public IActionResult SendMessage([FromBody] User SendMessageInputModel)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "game_results", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var stringMessage =
                 JsonSerializer.Serialize(SendMessageInputModel);
                    var byteArray = Encoding.UTF8.GetBytes(stringMessage);

                    channel.BasicPublish(exchange: "", routingKey: "game_results", basicProperties: null, body: byteArray);
                }
            }
            return Ok();
        }
        
        [HttpPost("iniciar Consumer")]
        public IActionResult StartConsumer ()
        {
            _configuration.IniciarFila();
            return Accepted();
        }
    }
}
