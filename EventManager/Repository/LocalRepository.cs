using System.Data;
using Dapper;
using EventManager.Data;
using EventManager.Repository.Interfaces;
using GestaoDeEventos.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace GestaoDeEventos.Repository
{
    public class LocalRepository : ILocalRepository
    {
        private readonly string _connectionString;

        public LocalRepository(IOptions<DatabaseConfig> config)
        {
            _connectionString = config.Value.Read;
        }


        public IEnumerable<Local> ObterTodos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Local>("SELECT * FROM Locais");
            }
        }

        public Local ObterPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Local>("SELECT * FROM Locais WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task Adicionar(Local local)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Locais (Nome, Endereco) 
                               VALUES (@Nome, @Endereco)";
                await connection.ExecuteAsync(sql, local);
            }
        }

        public async Task Atualizar(Local local)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Locais SET Nome = @Nome, Endereco = @Endereco WHERE Id = @Id";
                await connection.ExecuteAsync(sql, local);
            }
        }

        public async Task Deletar(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Locais WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
