using GestaoDeEventos.Models;

namespace EventManager.Repository.Interfaces
{
    public interface ILocalRepository
    {
        IEnumerable<Local> ObterTodos();
        Local ObterPorId(int id);

        Task Adicionar(Local local);
        Task Atualizar(Local local);
        Task Deletar(int id);
    }
}
