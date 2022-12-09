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
            Status statusDb = ListarPorId(status.Id);
            var statusDesativo = "desativado";
            statusDb.StatusEnum = statusDesativo;

            _dataContext.Statuses.Update(statusDb);
            await _dataContext.SaveChangesAsync();
            return statusDb;
        }
        public async Task<Status> Reativar(Status status)
        {
            Status statusDb = ListarPorId(status.Id);
            var statusReativado = "reativado";
            statusDb.StatusEnum = statusReativado;

            _dataContext.Statuses.Update(statusDb);
            await _dataContext.SaveChangesAsync();
            return statusDb;
        }

        public async Task<IEnumerable<Subscription>> Get()
        {
            _dataContext.Statuses.ToList();
            return await _dataContext.Subscriptions.ToListAsync();
        }

        public Status ListarPorId(int Id)
        {
            return _dataContext.Statuses.FirstOrDefault(x => x.Id == Id);
        }
    }
}
