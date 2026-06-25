using Dapper;
using PermissionHub.Models;

namespace PermissionHub
{
    public class UserRoleRepository
    {
        private readonly IDbConnectionFactory _factory;

        public UserRoleRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<int> AssignRoleAsync(Guid userId, Guid companyId ,Guid roleId)
        {
            using var db = _factory.Create();

            const string sql = @"
                INSERT INTO UserRoles
                (
                    UserId,
                    CompanyId,
                    RoleId
                )
                VALUES
                (
                    @UserId,
                    @CompanyId,
                    @RoleId
                )";

            return await db.ExecuteAsync(sql,
                new
                {
                    UserId = userId,
                    CompanyId = companyId,
                    RoleId = roleId
                });
        }

        public async Task<int> RemoveRoleAsync(Guid userId, Guid roleId)
        {
            using var db = _factory.Create();

            const string sql = @"
                DELETE FROM UserRoles
                WHERE UserId = @UserId
                  AND RoleId = @RoleId";

            return await db.ExecuteAsync(sql,
                new
                {
                    UserId = userId,
                    RoleId = roleId
                });
        }

        public async Task<List<Role>> GetRolesByUserIdAsync(Guid userId)
        {
            using var db = _factory.Create();

            const string sql = @"
                SELECT
                    r.Id,
                    r.CompanyId,
                    r.Name
                FROM Roles r
                INNER JOIN UserRoles ur
                    ON ur.RoleId = r.Id
                WHERE ur.UserId = @UserId
                ORDER BY r.Name";

            var roles = await db.QueryAsync<Role>(
                sql,
                new { UserId = userId });

            return roles.ToList();
        }
    }
}