using DesafioFinal.Controllers;
using DesafioFinal.Data;
using DesafioFinal.Models;
using DesafioFinal.Repositorio.MessageRepo;
using DesafioFinal.Repositorio.SubscriptionRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.UserRepo
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _dataContext;
      //  private readonly IMessageConfiguration _messageConfiguration;

        public UserRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
         //   _messageConfiguration = messageConfiguration;
         
        }

        public async Task<User> Create(User user)
        {
            
            var statusAtivo = "ativo";
            user.Subscription.Status.StatusEnum = statusAtivo;
            user.Subscription.CreatedAt = DateTime.Now;
            user.CreatedAt = DateTime.Now;

         

           // _messageConfiguration.Enviar(user);
            await Save(user);
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
            await _dataContext.SaveChangesAsync();
            //var newUser = _messageConfiguration.Receber(user);
            return user;
        }
    }
}
