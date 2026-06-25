using Dapper;
using PermissionHub.Models;
namespace PermissionHub
{
    public class RolePermissionRepository
    {
        private readonly IDbConnectionFactory _factory;

        public RolePermissionRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        // GET existing permissions of a role
        public async Task<List<RolePagePermission>> GetByRoleAsync(Guid roleId)
        {
            using var db = _factory.Create();

            var sql = @"
            SELECT * 
            FROM RolePages
            WHERE RoleId = @RoleId";

            return (await db.QueryAsync<RolePagePermission>(sql, new { RoleId = roleId }))
                .ToList();
        }

        // ASSIGN permission
        public async Task AssignAsync(Guid roleId, string moduleCode, string pageCode)
        {
            using var db = _factory.Create();

            var sql = @"
            IF NOT EXISTS (
                SELECT 1 FROM RolePages
                WHERE RoleId = @RoleId
                  AND ModuleCode = @ModuleCode
                  AND PageCode = @PageCode
            )
            INSERT INTO RolePages(RoleId, ModuleCode, PageCode, CanAccess)
            VALUES(@RoleId, @ModuleCode, @PageCode, 1)";

            await db.ExecuteAsync(sql, new
            {
                RoleId = roleId,
                ModuleCode = moduleCode,
                PageCode = pageCode
            });
        }

        // REMOVE permission
        public async Task RemoveAsync(Guid roleId, string moduleCode, string pageCode)
        {
            using var db = _factory.Create();

            var sql = @"
            DELETE FROM RolePages
            WHERE RoleId = @RoleId
              AND ModuleCode = @ModuleCode
              AND PageCode = @PageCode";

            await db.ExecuteAsync(sql, new
            {
                RoleId = roleId,
                ModuleCode = moduleCode,
                PageCode = pageCode
            });
        }
    }
}
