using DesafioFinal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.SubscriptionRepo
{
    public interface ISubscriptionRepo
    {
        Task<IEnumerable<Subscription>> Get();
        Task<Subscription> NewSubscription(int id);
        Task<Status> Desativar(Status status);
        Task<Status> Reativar(Status status);
        Status ListarStatusPorId(int Id);
        Task<IEnumerable<Subscription>> ListarSubPorId(int Id);
    }
}
