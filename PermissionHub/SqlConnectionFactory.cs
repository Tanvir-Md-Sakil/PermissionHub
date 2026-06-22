using Microsoft.Data.SqlClient;
using System.Data;
namespace PermissionHub
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;

        public SqlConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Create()
        {
            return new SqlConnection(
                _config.GetConnectionString("DefaultConnection"));
        }
    }
}
