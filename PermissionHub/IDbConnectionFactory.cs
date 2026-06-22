using System.Data;

namespace PermissionHub
{

    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}
