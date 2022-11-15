using DesafioFinal.Models;
using DesafioFinal.Repositorio.MessageRepo;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DesafioFinal.Controllers
{
    [Route("api/messages")]
    public class MessageController : Controller
    {
        private const string QUEUE_NAME = "messages";
        private readonly ConnectionFactory _factory;
        private readonly IMessageConfiguration _configuration;
        public MessageController(IMessageConfiguration configuration)
        {
           
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult SendMessage([FromBody] MessageModel SendMessageInputModel)
        {
            _configuration.Enviar(SendMessageInputModel);
            return Accepted();
        }
    }
}
