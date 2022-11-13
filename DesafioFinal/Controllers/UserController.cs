using DesafioFinal.Models;
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
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
       public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepo.Get();
        }

        [HttpPost("Criar/{FullName}")]
        public async Task<User> Create([FromBody]  User user, string FullName)
        {
            var newUser = await _userRepo.Create(user, FullName);
            return newUser;
        }
    }
}
