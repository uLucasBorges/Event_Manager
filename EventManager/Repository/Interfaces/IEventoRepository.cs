using GestaoDeEventos.Models;

namespace EventManager.Repository.Interfaces
{
    public interface IEventoRepository
    {
        IEnumerable<Evento> ObterTodos();
        Evento ObterPorId(int id);
        Task Adicionar(Evento evento);
        Task Atualizar(Evento evento);
        Task Deletar(int id);
    }
}
