using DesafioFinal.Models;
using System.Threading.Tasks;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public interface IMessageConfiguration
    {
        User Enviar(User messageModel);
        Task IniciarFila();

    }
}
