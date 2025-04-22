using Dapper;
using EventManager.Repository.Interfaces;
using GestaoDeEventos.Models;
using GestaoDeEventos.Repository.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GestaoDeEventos.Repository
{
    public class ParticipanteRepository : IParticipanteRepository
    {

        private DbSession _Dbconfiguration;

        public ParticipanteRepository(DbSession configuration)
        {
            _Dbconfiguration = configuration;
        }

        public IEnumerable<Participante> ObterTodos()
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                return connection.Query<Participante>("SELECT * FROM Participantes");
            }
        }

        public IEnumerable<Participante> ObterPorEvento(int eventoId)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                return connection.Query<Participante>("SELECT * FROM Participantes WHERE EventoId = @EventoId", new { EventoId = eventoId });
            }
        }

        public async Task Adicionar(Participante participante)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                string sql = "INSERT INTO Participantes (Nome, Email, EventoId) VALUES (@Nome, @Email, @EventoId)";
                await connection.ExecuteAsync(sql, participante);
            }
        }

        public async Task Atualizar(Participante participante)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                string sql = "UPDATE Participantes SET Nome = @Nome, Email = @Email, EventoId = @EventoId WHERE Id = @Id";
                await connection.ExecuteAsync(sql, participante);
            }
        }

        public async Task Deletar(int id)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                string sql = "DELETE FROM Participantes WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

      
    }
}
