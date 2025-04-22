using GestaoDeEventos.Models;

namespace EventManager.Repository.Interfaces
{
    public interface IParticipanteRepository
    {
        IEnumerable<Participante> ObterTodos();
        IEnumerable<Participante> ObterPorEvento(int eventoId);
        Task Adicionar(Participante participante);
        Task Atualizar(Participante participante);
        Task Deletar(int id);
    }
}
