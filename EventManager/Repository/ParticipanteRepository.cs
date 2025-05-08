using Dapper;
using EventManager.Data;
using EventManager.Repository.Interfaces;
using GestaoDeEventos.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace GestaoDeEventos.Repository
{
    public class ParticipanteRepository : IParticipanteRepository
    {

        private readonly string _connectionString;

        public ParticipanteRepository(IOptions<DatabaseConfig> config)
        {
            _connectionString = config.Value.Read;
        }

        public IEnumerable<Participante> ObterTodos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Participante>("SELECT * FROM Participantes");
            }
        }

        public IEnumerable<Participante> ObterPorEvento(int eventoId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Participante>("SELECT * FROM Participantes WHERE EventoId = @EventoId", new { EventoId = eventoId });
            }
        }

        public async Task Adicionar(Participante participante)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Participantes (Nome, Email, EventoId) VALUES (@Nome, @Email, @EventoId)";
                await connection.ExecuteAsync(sql, participante);
            }
        }

        public async Task Atualizar(Participante participante)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Participantes SET Nome = @Nome, Email = @Email, EventoId = @EventoId WHERE Id = @Id";
                await connection.ExecuteAsync(sql, participante);
            }
        }

        public async Task Deletar(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Participantes WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

      
    }
}
