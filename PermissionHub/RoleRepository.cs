using Dapper;
using PermissionHub.Models;

namespace PermissionHub
{
    public class RoleRepository
    {
        private readonly IDbConnectionFactory _factory;

        public RoleRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            using var db = _factory.Create();

            const string sql = @"
            SELECT
                Id,
                Name
            FROM Roles
            ORDER BY Name";

            var roles = await db.QueryAsync<Role>(sql);

            return roles.ToList();
        }

        public async Task<Role?> GetByIdAsync(Guid id)
        {
            using var db = _factory.Create();

            const string sql = @"
            SELECT
                Id,
                Name
            FROM Roles
            WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Role>(
                sql,
                new { Id = id });
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            using var db = _factory.Create();

            const string sql = @"
            SELECT
                Id,
                Name
            FROM Roles
            WHERE Name = @Name";

            return await db.QueryFirstOrDefaultAsync<Role>(
                sql,
                new { Name = name });
        }

        public async Task<int> CreateAsync(Role role)
        {
            using var db = _factory.Create();

            const string sql = @"
            INSERT INTO Roles
            (
                Id,
                Name
            )
            VALUES
            (
                @Id,
                @Name
            )";

            return await db.ExecuteAsync(sql, role);
        }

        public async Task<int> UpdateAsync(Role role)
        {
            using var db = _factory.Create();

            const string sql = @"
            UPDATE Roles
            SET
                Name = @Name
            WHERE Id = @Id";

            return await db.ExecuteAsync(sql, role);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            using var db = _factory.Create();

            const string sql = @"
            DELETE FROM Roles
            WHERE Id = @Id";

            return await db.ExecuteAsync(
                sql,
                new { Id = id });
        }

        public async Task<bool> ExistsAsync(string name)
        {
            using var db = _factory.Create();

            const string sql = @"
            SELECT COUNT(*)
            FROM Roles
            WHERE Name = @Name";

            var count = await db.ExecuteScalarAsync<int>(
                sql,
                new { Name = name });

            return count > 0;
        }
    }
}
