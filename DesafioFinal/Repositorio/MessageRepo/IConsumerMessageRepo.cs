using DesafioFinal.Models;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public interface IConsumerMessageRepo
    {
        void IniciarFilaCriar();
        void IniciarFilaDesativar();
        void IniciarFilaReativar();
    }
}
