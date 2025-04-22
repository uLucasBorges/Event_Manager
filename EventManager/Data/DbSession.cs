using System.Data;
using System.Data.SqlClient;

namespace GestaoDeEventos.Repository.Data
{
    public class DbSession : IDisposable
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration
                .GetConnectionString("Default"));
            Connection.Open();

           
        }

        public void Dispose() => Connection?.Dispose();
    }
}
