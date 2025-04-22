using Dapper;
using EventManager.Repository.Interfaces;
using GestaoDeEventos.Models;
using GestaoDeEventos.Repository.Data;
using Microsoft.Data.SqlClient;

namespace GestaoDeEventos.Repository
{
    public class EventoRepository : IEventoRepository
    {
        private DbSession _Dbconfiguration;

        public EventoRepository(DbSession configuration)
        {
            _Dbconfiguration = configuration;
        }

        public IEnumerable<Evento> ObterTodos()
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                var resultado = connection.Query<Evento>("SELECT * FROM Eventos");

                return resultado;
            }
        }

        public Evento ObterPorId(int id)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                return connection.QueryFirstOrDefault<Evento>("SELECT * FROM Eventos WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task Adicionar(Evento evento)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                string sql = "INSERT INTO Eventos (Nome, Data, LocalId, CapacidadeMaxima) VALUES (@Nome, @Data, @LocalId, @CapacidadeMaxima)";
                await connection.ExecuteAsync(sql, evento);

            }
        }
        public async Task Atualizar(Evento evento)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                string sql = "UPDATE Eventos SET Nome = @Nome, Data = @Data, LocalId = @LocalId, CapacidadeMaxima = @CapacidadeMaxima WHERE Id = @Id";
                await connection.ExecuteAsync(sql, evento);
            }
        }

        public async Task Deletar(int id)
        {
            using (var connection = _Dbconfiguration.Connection)
            {
                string sql = "DELETE FROM Eventos WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
