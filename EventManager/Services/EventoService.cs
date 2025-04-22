using EventManager.Repository.Interfaces;
using EventManager.Services.Interfaces;
using GestaoDeEventos.Models;
using GestaoDeEventos.Repository;

namespace GestaoDeEventos.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }


        public IEnumerable<Evento> ListarEventos()
        {
            return _eventoRepository.ObterTodos().ToList();
        }

        public Evento ObterEventoPorId(int id)
        {
            return _eventoRepository.ObterPorId(id);
        }

        public async Task CriarEvento(Evento evento)
        {
            // Regra de negócio: Não permitir eventos no passado
            if (evento.Data < DateTime.Now)
            {
                throw new ArgumentException("Erro: A data do evento não pode estar no passado.");
            }

            await _eventoRepository.Adicionar(evento);
        }

        public async Task AtualizarEvento(Evento evento)
        {
            if (evento.Data < DateTime.Now)
            {
                throw new ArgumentException("Erro: Não é possível atualizar eventos que ocorreram no passado.");
            }

            await _eventoRepository.Atualizar(evento);
        }

        public async Task DeletarEvento(int id)
        {
            await _eventoRepository.Deletar(id);
        }
    }
}
