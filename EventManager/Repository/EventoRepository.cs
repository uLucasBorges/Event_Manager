using System.Data.SqlClient;
using Dapper;
using EventManager.Data;
using EventManager.Repository.Interfaces;
using GestaoDeEventos.Models;
using Microsoft.Extensions.Options;

namespace GestaoDeEventos.Repository
{
    public class EventoRepository : IEventoRepository
    {
        private readonly string _connectionString;

        public EventoRepository(IOptions<DatabaseConfig> config)
        {
            _connectionString = config.Value.Read;
        }


        public IEnumerable<Evento> ObterTodos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var resultado = connection.Query<Evento>("SELECT * FROM Eventos");

                return resultado;
            }
        }

        public Evento ObterPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Evento>("SELECT * FROM Eventos WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task Adicionar(Evento evento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Eventos (Nome, Data, LocalId, CapacidadeMaxima) VALUES (@Nome, @Data, @LocalId, @CapacidadeMaxima)";
                await connection.ExecuteAsync(sql, evento);

            }
        }
        public async Task Atualizar(Evento evento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Eventos SET Nome = @Nome, Data = @Data, LocalId = @LocalId, CapacidadeMaxima = @CapacidadeMaxima WHERE Id = @Id";
                await connection.ExecuteAsync(sql, evento);
            }
        }

        public async Task Deletar(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Eventos WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
