using DesafioFinal.Controllers;
using DesafioFinal.Data;
using DesafioFinal.Models;
using DesafioFinal.Repositorio.MessageRepo;
using DesafioFinal.Repositorio.SubscriptionRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.UserRepo
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _dataContext;
        private readonly IMessageConfiguration _messageConfiguration;

        public UserRepo(DataContext dataContext, IMessageConfiguration messageConfiguration)
        {
            _dataContext = dataContext;
            _messageConfiguration = messageConfiguration;
         
        }

        public async Task<User> Create(User user, string FullName)
        {
            ;
            var statusAtivo = "ativo";
            user.Subscription.Status.StatusEnum = statusAtivo;
            user.FullName = FullName;
           
          
            _messageConfiguration.Enviar(user);
            Save(user);
            return user;
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<User> Save(User user)
        {
             //var newUser = _messageConfiguration.Receber(user);
            
            _dataContext.Add(user);
            _dataContext.SaveChangesAsync();
            var newUser = _messageConfiguration.Receber(user);
            return newUser;
        }
    }
}
