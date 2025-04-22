using GestaoDeEventos.Models;

namespace EventManager.Services.Interfaces
{
    public interface IEventoService
    {
        IEnumerable<Evento> ListarEventos();
        Evento ObterEventoPorId(int id);
        Task CriarEvento(Evento evento);
        Task AtualizarEvento(Evento evento);
        Task DeletarEvento(int id);
    }
}
