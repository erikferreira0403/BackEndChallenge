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
        public UserRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<User> Create(User user)
        {
            user.CreatedAt = DateTime.Now;
            _dataContext.Add(user);
            await _dataContext.SaveChangesAsync();
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
    }
}
