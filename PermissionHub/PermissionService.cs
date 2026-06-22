namespace PermissionHub
{
    using Dapper;

    public class PermissionService
    {
        private readonly IDbConnectionFactory _factory;

        public PermissionService(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> HasAccess(Guid userId, string url)
        {
            using var db = _factory.Create();

            var sql = @"
        SELECT COUNT(*)
        FROM UserRoles ur
        INNER JOIN RolePermissions rp ON ur.RoleId = rp.RoleId
        INNER JOIN Pages p ON p.Id = rp.PageId
        WHERE ur.UserId = @UserId
        AND p.Url = @Url
        AND rp.CanView = 1";

            return await db.ExecuteScalarAsync<int>(
                sql,
                new { UserId = userId, Url = url }) > 0;
        }
    }
}
