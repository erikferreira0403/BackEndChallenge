using DesafioFinal.Models;

namespace DesafioFinal.Repositorio.MessageRepo
{
    public interface ISendMessageRepo
    {
        User CriarUsuário(User messageModel);
        Status DesativarUsuário(int Id, Status status);
        Status ReativarUsuário(int Id, Status status);
    }
}
