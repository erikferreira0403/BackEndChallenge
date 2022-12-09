using DesafioFinal.Models;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public interface IConsumerMessageRepo
    {
        Task IniciarFilaCriar();
        Task IniciarFilaDesativar();
        Task IniciarFilaReativar();
    }
}
