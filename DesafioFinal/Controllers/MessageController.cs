using DesafioFinal.Models;
using DesafioFinal.Repositorio.MessageRepo;
using DesafioFinal.Repositorio.SubscriptionRepo;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesafioFinal.Controllers
{
    [Route("api/messages")]
    public class MessageController : Controller
    {
       
        private readonly IConsumerMessageRepo _configuration;
        private readonly ISendMessageRepo _messageRepo;
        public MessageController(IConsumerMessageRepo configuration, ISendMessageRepo messageRepo)
        {
            _configuration = configuration;
           
            _messageRepo = messageRepo;
        }

        [HttpPost("Criar Usuário")]
        public IActionResult SendMessage([FromBody] User SendMessageInputModel)
        {
            _messageRepo.CriarUsuário(SendMessageInputModel);
            return Ok();
        }

        [HttpPut("Desativar/{id}")]
        public IActionResult DesativarSubscription(int id, [FromBody] Status status)
        {
            _messageRepo.DesativarUsuário(id, status);
            return Ok();
        }

        [HttpPut("Reativar/{id}")]
        public IActionResult ReativarSubscription(int id, [FromBody] Status status)
        {
            _messageRepo.ReativarUsuário(id, status);
            return Ok();
        }

        [HttpPost("iniciar Consumer")]
        public IActionResult StartConsumer ()
        {
            _configuration.IniciarFilaCriar();
            Task.Delay(60000).Wait();
            return Ok();
        }
        [HttpPut("iniciar Consumer Desativar")]
        public IActionResult StartConsumerDesativar()
        {
            _configuration.IniciarFilaDesativar();
            Task.Delay(60000).Wait();
            return Ok();
        }

        [HttpPut("iniciar Consumer Reativar")]
        public IActionResult StartConsumerReativar()
        {
            _configuration.IniciarFilaReativar();
            Task.Delay(60000).Wait();
            return Ok();
        }
    }
}
