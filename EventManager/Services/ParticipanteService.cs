using EventManager.Repository.Interfaces;
using EventManager.Services.Interfaces;
using GestaoDeEventos.Models;
using GestaoDeEventos.Repository;

namespace GestaoDeEventos.Services
{
    public class ParticipanteService : IParticipanteService
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IEventoRepository _eventoRepository;

        public ParticipanteService(IParticipanteRepository participanteRepository, IEventoRepository eventoRepository)
        {
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
        }

        public IEnumerable<Participante> ListarParticipantes()
        {
            return _participanteRepository.ObterTodos();
        }

        public IEnumerable<Participante> ListarParticipantesPorEvento(int eventoId)
        {
            return _participanteRepository.ObterPorEvento(eventoId);
        }

        public async Task AdicionarParticipante(string nome, string email, int eventoId)
        {
            var evento = _eventoRepository.ObterPorId(eventoId);
            if (evento == null)
            {
                throw new ArgumentException("Erro: Evento não encontrado.");
            }

            var participantes = _participanteRepository.ObterPorEvento(eventoId);

            // Regra de negócio: Verificar se o evento já atingiu a capacidade máxima
            if (participantes.Count() >= evento.CapacidadeMaxima)
            {
                throw new ArgumentException("Erro: O evento atingiu a capacidade máxima.");
            }

            var novoParticipante = new Participante { Nome = nome, Email = email, EventoId = eventoId };
            await _participanteRepository.Adicionar(novoParticipante);
        }

        public async Task AtualizarParticipante(Participante participante)
        {
            var existente = _participanteRepository.ObterTodos().FirstOrDefault(p => p.Id == participante.Id);
            if (existente == null)
            {
                throw new ArgumentException("Erro: Participante não encontrado.");
            }

            await _participanteRepository.Atualizar(participante);
        }

        public async Task DeletarParticipante(int id)
        {
            var existente = _participanteRepository.ObterTodos().FirstOrDefault(p => p.Id == id);
            if (existente == null)
            {
                throw new ArgumentException("Erro: Participante não encontrado.");
            }

            await _participanteRepository.Deletar(id);
        }
    }
}
