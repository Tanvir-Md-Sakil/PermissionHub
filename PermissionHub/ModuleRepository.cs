using Dapper;
using PermissionHub.Models;

namespace PermissionHub
{
        public class ModuleRepository
        {
            private readonly IDbConnectionFactory _factory;

            public ModuleRepository(IDbConnectionFactory factory)
            {
                _factory = factory;
            }

            public async Task<List<Module>> GetAllAsync()
            {
                using var db = _factory.Create();

                const string sql = @"
                SELECT
                    Id,
                    Code,
                    Name
                FROM Modules
                ORDER BY Name";

                var modules = await db.QueryAsync<Module>(sql);

                return modules.ToList();
            }

            public async Task<Module?> GetByIdAsync(Guid id)
            {
                using var db = _factory.Create();

                const string sql = @"
                SELECT
                    Id,
                    Code,
                    Name
                FROM Modules
                WHERE Id = @Id";

                return await db.QueryFirstOrDefaultAsync<Module>(
                    sql,
                    new { Id = id });
            }

            public async Task<Module?> GetByCodeAsync(string code)
            {
                using var db = _factory.Create();

                const string sql = @"
                SELECT
                    Id,
                    Code,
                    Name
                FROM Modules
                WHERE Code = @Code";

                return await db.QueryFirstOrDefaultAsync<Module>(
                    sql,
                    new { Code = code });
            }

            public async Task<int> CreateAsync(Module module)
            {
                using var db = _factory.Create();

                const string sql = @"
                INSERT INTO Modules
                (
                    Id,
                    Code,
                    Name
                )
                VALUES
                (
                    @Id,
                    @Code,
                    @Name
                )";

                return await db.ExecuteAsync(sql, module);
            }

            public async Task<int> DeleteAsync(Guid id)
            {
                using var db = _factory.Create();

                const string sql = @"
                DELETE FROM Modules
                WHERE Id = @Id";

                return await db.ExecuteAsync(
                    sql,
                    new { Id = id });
            }
        }
}
