using DesafioFinal.Data;
using DesafioFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.SubscriptionRepo
{
    public class SubscriptionRepo : ISubscriptionRepo
    {
        private readonly DataContext _dataContext;
        public SubscriptionRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Status> Desativar(Status status)
        {
            Status statusDb = ListarStatusPorId(status.Id);
            var statusDesativo = "desativado";
            statusDb.StatusEnum = statusDesativo;
            
            _dataContext.Statuses.Update(statusDb);
            await _dataContext.SaveChangesAsync();
            return statusDb;
        }
        public async Task<Status> Reativar(Status status)
        {
            Status statusDb = ListarStatusPorId(status.Id);
            var statusReativado = "reativado";
            statusDb.StatusEnum = statusReativado;

            _dataContext.Statuses.Update(statusDb);
            await _dataContext.SaveChangesAsync();
            return statusDb;
        }

        public async Task<IEnumerable<Subscription>> Get()
        {
            await _dataContext.SaveChangesAsync();
            _dataContext.Statuses.ToList();
            return await _dataContext.Subscriptions.ToListAsync();
        }

        public Status ListarStatusPorId(int Id)
        {
            return _dataContext.Statuses.FirstOrDefault(x => x.Id == Id);
        }

        public async Task<Subscription> NewSubscription(int id)
        {
            Subscription newSubscription = new Subscription();
            Status newStatus = new Status();
            EventHistory eventHistory = new EventHistory();
            newSubscription.UserId = id;
            newSubscription.Status = newStatus;
            newSubscription.Status.StatusEnum = "ativo";
            newSubscription.EventHistory = eventHistory;

            _dataContext.Add(newSubscription);
            await _dataContext.SaveChangesAsync();
            return newSubscription;
        }

        public async Task<IEnumerable<Subscription>> ListarSubPorId(int Id)
        {
            return _dataContext.Subscriptions.Where(x => x.UserId == Id).ToList();  
        }
    }
}
