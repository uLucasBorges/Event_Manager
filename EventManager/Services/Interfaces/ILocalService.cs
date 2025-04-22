using GestaoDeEventos.Models;

namespace EventManager.Services.Interfaces
{
    public interface ILocalService
    {
        IEnumerable<Local> ListarLocais();
        Task CriarLocal(Local local);
        Task AtualizarLocal(Local local);
        Task DeletarLocal(int id);
    }
}
