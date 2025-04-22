using GestaoDeEventos.Models;

namespace EventManager.Services.Interfaces
{
    public interface IParticipanteService
    {
        IEnumerable<Participante> ListarParticipantesPorEvento(int eventoId);
        Task AdicionarParticipante(string nome, string email, int eventoId);
        Task AtualizarParticipante(Participante participante);
        Task DeletarParticipante(int id);
    }
}
