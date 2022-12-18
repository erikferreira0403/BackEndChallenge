using DesafioFinal.Models;
using DesafioFinal.Repositorio.SubscriptionRepo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFinal.Controllers
{

    [Route("api/Subscription")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepo _repositorio;

        public SubscriptionController(ISubscriptionRepo repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IEnumerable<Subscription>> GetSubscriptions()
        {
            return await _repositorio.Get();
        }
        [HttpGet("{Id}")]
        public async Task<IEnumerable<Subscription>> GetSubById(int Id)
        {
            return await _repositorio.ListarSubPorId(Id);
        }
        [HttpPut("Desativar/{id}")]
        public async Task<Status> DesativarSubscription(int id, [FromBody] Status status)
        {
            status.Id = id;
            var newsub = await _repositorio.NewSubscription(id);
            await _repositorio.Desativar(newsub.Status);
            return newsub.Status;
        }

        [HttpPost("Reativar/{id}")]
        public async Task<Status> ReativarSubscription( int id, [FromBody] Status status)
        {
            status.Id = id;
            var newsub = await _repositorio.NewSubscription(id);
            await _repositorio.Reativar(newsub.Status);
            return newsub.Status;
        }
    }
    
}
