using DesafioFinal.Data;
using DesafioFinal.Models;
using DesafioFinal.Repositorio.UserRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.SubscriptionRepo
{
    public class SubscriptionRepo : ISubscriptionRepo
    {
        private readonly DataContext _dataContext;
        private readonly IUserRepo _userRepo;
        public SubscriptionRepo(DataContext dataContext, IUserRepo userRepo)
        {
            _dataContext = dataContext;
            _userRepo = userRepo;
        }

        public async Task<Status> Desativar(Status status)
        {
            var Verify = ListarSubPorId(status.Id);
            if (Verify == null)
                throw new System.Exception("Houve um erro na criação da subscription");

            Status statusDb = ListarStatusPorId(status.Id);
            statusDb.StatusEnum = "desativado";
            
            _dataContext.Statuses.Update(statusDb);
            await _dataContext.SaveChangesAsync();
            return statusDb;
        }
        public async Task<Status> Reativar(Status status)
        {
            var Verify = ListarSubPorId(status.Id);
            if (Verify == null)
                throw new System.Exception("Houve um erro na criação da subscription");


            Status statusDb = ListarStatusPorId(status.Id);
            statusDb.StatusEnum = "reativado";

            _dataContext.Statuses.Update(statusDb);
            await _dataContext.SaveChangesAsync();
            return statusDb;
        }
        public async Task<Subscription> NewSubscription(int id)
        {
            User user = _userRepo.ListarPorId(id);
            if (user == null)
                throw new System.Exception("Houve um erro na criação da subscription");

            Subscription newSubscription = new();
            newSubscription.UserId = id;
            newSubscription.Status = new()
            {
                StatusEnum = "ativo"
            };
            newSubscription.EventHistory = new();
            newSubscription.CreatedAt = System.DateTime.Now;

            _dataContext.Add(newSubscription);
            await _dataContext.SaveChangesAsync();
            return newSubscription;
        }
        public async Task<IEnumerable<Subscription>> Get()
        {
            await _dataContext.SaveChangesAsync();
            _dataContext.Statuses.ToList();
            return await _dataContext.Subscriptions.ToListAsync();
        }
        public async Task<IEnumerable<Subscription>> ListarSubPorId(int Id)
        {
           var data = _dataContext.Subscriptions.Where(x => x.UserId == Id && x.UserId != 0).ToList();
            if (data.Count() >= 1)
            {
                return data;
            }
            throw new System.Exception("erro ao listar");
        }
        public Status ListarStatusPorId(int Id)
        {
            return _dataContext.Statuses.FirstOrDefault(x => x.Id == Id);
        }
    }
}
