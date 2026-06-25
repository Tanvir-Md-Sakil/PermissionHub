using Dapper;

namespace PermissionHub
{
    public class PermissionService
    {
        private readonly IDbConnectionFactory _factory;
        private readonly CompanyModuleRepository _companyModuleRepo;

        public PermissionService(
            IDbConnectionFactory factory,
            CompanyModuleRepository companyModuleRepo)
        {
            _factory = factory;
            _companyModuleRepo = companyModuleRepo;
        }

        public async Task<bool> CanAccessPageAsync(
            Guid userId,
            Guid companyId,
            string moduleCode,
            string pageCode)
        {
            // 1. Check company module access
            bool moduleEnabled =
                await _companyModuleRepo.HasAccessAsync(companyId, moduleCode);

            if (!moduleEnabled)
                return false;

            // 2. Check role + page permission
            using var db = _factory.Create();

            var sql = @"
                SELECT COUNT(1)
                FROM UserRoles ur
                INNER JOIN RolePages rp ON rp.RoleId = ur.RoleId
                WHERE ur.UserId = @UserId
                  AND ur.CompanyId = @CompanyId
                  AND rp.ModuleCode = @ModuleCode
                  AND rp.PageCode = @PageCode
                  AND rp.CanAccess = 1";

            return await db.ExecuteScalarAsync<int>(sql, new
            {
                UserId = userId,
                CompanyId = companyId,
                ModuleCode = moduleCode,
                PageCode = pageCode
            }) > 0;
        }
    }
}