using GestaoDeEventos.Models;

namespace EventManager.Repository.Interfaces
{
    public interface ILocalRepository
    {
        IEnumerable<Local> ObterTodos();
        Task Adicionar(Local local);
        Task Atualizar(Local local);
        Task Deletar(int id);
    }
}
