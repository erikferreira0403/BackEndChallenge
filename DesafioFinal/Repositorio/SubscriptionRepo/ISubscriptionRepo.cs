using DesafioFinal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.SubscriptionRepo
{
    public interface ISubscriptionRepo
    {
        Task<IEnumerable<Subscription>> Get();
        Task<Status> Desativar(Status status);
        Task<Status> Reativar(Status status);
        Status ListarPorId(int Id);
    }
}
