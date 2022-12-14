using DesafioFinal.Controllers;
using DesafioFinal.Data;
using DesafioFinal.Models;
using DesafioFinal.Repositorio.MessageRepo;
using DesafioFinal.Repositorio.SubscriptionRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.UserRepo
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _dataContext;
        private readonly ISubscriptionRepo _subscriptionRepo;
        public UserRepo(DataContext dataContext, ISubscriptionRepo subscriptionRepo)
        {
            _dataContext = dataContext;
            _subscriptionRepo = subscriptionRepo;
        }
        public async Task<User> Create(User user)
        {
            await Save(user);
            return user;
        }
        public async Task<IEnumerable<User>> Get()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public User ListarPorId(int Id)
        {
            return _dataContext.Users.Find(Id);
        }

        public int NewSubUser(int id)
        {
             _subscriptionRepo.NewSubscription(id);
            return id;
        }

        public async Task<User> Save(User user)
        {
            _dataContext.Add(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }
}
