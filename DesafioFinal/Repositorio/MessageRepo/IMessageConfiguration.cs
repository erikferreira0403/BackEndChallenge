using DesafioFinal.Models;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public interface IMessageConfiguration
    {
        User Enviar(User messageModel);
        User Receber(User messageModel);
    }
}
