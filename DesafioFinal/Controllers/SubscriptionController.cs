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

        [HttpPost("Desativar/{id}")]
        public async Task<Status> DesativarSubscription(int id, Status status)
        {
            status.Id = id;
            var newStatus = await _repositorio.Desativar(status);
            return newStatus;
        }

        [HttpPost("Reativar/{id}")]
        public async Task<Status> ReativarSubscription(int id, Status status)
        {
            status.Id = id;
            var newStatus = await _repositorio.Reativar(status);
            return newStatus;
        }
    }
}
