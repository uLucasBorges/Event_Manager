using EventManager.Repository.Interfaces;
using EventManager.Services.Interfaces;
using GestaoDeEventos.Models;
using GestaoDeEventos.Repository;

namespace GestaoDeEventos.Services
{
    public class LocalService : ILocalService 
    {
        private readonly ILocalRepository _localRepository;

        public LocalService(ILocalRepository localRepository)
        {
            _localRepository = localRepository;
        }

        public IEnumerable<Local> ListarLocais()
        {
            return _localRepository.ObterTodos();
        }

        public async Task CriarLocal(Local novoLocal)
        {
            // Regra de negócio: Verifica se o nome do local já existe
            var locais = _localRepository.ObterTodos();
            if (locais.Any(local => local.Nome == novoLocal.Nome))
            {
                throw new ArgumentException("Erro: Já existe um local com esse nome.");
            }

            await _localRepository.Adicionar(novoLocal);
        }

        public async Task AtualizarLocal(Local local)
        {
            var existente = _localRepository.ObterTodos().FirstOrDefault(l => l.Id == local.Id);
            if (existente == null)
            {
                throw new ArgumentException("Erro: Local não encontrado.");
            }

            await _localRepository.Atualizar(local);
        }

        public async Task DeletarLocal(int id)
        {
            var existente = _localRepository.ObterPorId(id);
            if (existente == null)
            {
                throw new ArgumentException("Erro: Local não encontrado.");
            }

            await _localRepository.Deletar(id);
        }

        public Local ObterLocalPorId(int id)
        {
            return _localRepository.ObterPorId(id);
        }

      

    }
}
