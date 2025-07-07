using Npgsql;

namespace WebApplication2.Data
{
    public class DbHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
