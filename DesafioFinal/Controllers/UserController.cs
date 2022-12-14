using DesafioFinal.Models;
using DesafioFinal.Repositorio.SubscriptionRepo;
using DesafioFinal.Repositorio.UserRepo;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFinal.Controllers
{
    [Route("api/User")]
  
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly ISubscriptionRepo _subscriptionRepo;
        public UserController(IUserRepo userRepo, ISubscriptionRepo subscriptionRepo)
        {
            _userRepo = userRepo;
            _subscriptionRepo = subscriptionRepo;
        }

        [HttpGet]
       public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepo.Get();
        }
        [HttpGet("Get By Id/{id}")]
        public async Task<User> GetUserbyId(int id)
        {
            return _userRepo.ListarPorId(id);
        }
        [HttpPost("Criar/{FullName}")]
        public async Task<User> Create([FromBody]  User user, string FullName)
        {
            var newUser = await _userRepo.Create(user);
            await _subscriptionRepo.NewSubscription(newUser.Id);
            return newUser;
        }
    }
}
