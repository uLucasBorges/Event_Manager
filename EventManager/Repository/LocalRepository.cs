using Dapper;
using EventManager.Repository.Interfaces;
using GestaoDeEventos.Models;
using GestaoDeEventos.Repository.Data;
using Microsoft.Data.SqlClient;

namespace GestaoDeEventos.Repository
{
    public class LocalRepository : ILocalRepository
    {
        private DbSession _Dbconfiguration;

        public LocalRepository(DbSession configuration)
        {
            _Dbconfiguration = configuration;
        }

        public IEnumerable<Local> ObterTodos()
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                return connection.Query<Local>("SELECT * FROM Locais");
            }
        }

        public async Task Adicionar(Local local)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                string sql = @"INSERT INTO Eventos (Nome, Data, LocalId, CapacidadeMaxima, ImagemUrl, Descricao, Feedback) 
               VALUES (@Nome, @Data, @LocalId, @CapacidadeMaxima, @ImagemUrl, @Descricao, @Feedback)";
                await connection.ExecuteAsync(sql, local);
            }
        }

        public async Task Atualizar(Local local)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                string sql = "UPDATE Locais SET Nome = @Nome, Endereco = @Endereco, Capacidade = @Capacidade WHERE Id = @Id";
                await connection.ExecuteAsync(sql, local);
            }
        }

        public async Task Deletar(int id)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                string sql = "DELETE FROM Locais WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
