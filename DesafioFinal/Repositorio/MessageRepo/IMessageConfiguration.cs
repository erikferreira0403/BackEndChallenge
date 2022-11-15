using DesafioFinal.Models;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public interface IMessageConfiguration
    {
        MessageModel Enviar(MessageModel messageModel);
    }
}
