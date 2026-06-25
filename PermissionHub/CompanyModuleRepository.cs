using Dapper;
using PermissionHub.Models;

namespace PermissionHub
{
    public class CompanyModuleRepository
    {
        private readonly IDbConnectionFactory _factory;

        public CompanyModuleRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> HasAccessAsync(
            Guid companyId,
            string moduleCode)
        {
            using var db = _factory.Create();

            const string sql = @"
                SELECT COUNT(1)
                FROM CompanyModules cm
                INNER JOIN Modules m
                    ON m.Id = cm.ModuleId
                WHERE cm.CompanyId = @CompanyId
                  AND m.Code = @Code
                  AND cm.IsEnabled = 1";

            return await db.ExecuteScalarAsync<int>(
                sql,
                new
                {
                    CompanyId = companyId,
                    Code = moduleCode
                }) > 0;
        }

        public async Task<List<ModuleViewModel>>
            GetModulesForCompanyAsync(Guid companyId)
        {
            using var db = _factory.Create();

            const string sql = @"
                SELECT
                    m.Id,
                    m.Name,
                    m.Code,
                    CASE
                        WHEN cm.CompanyId IS NULL
                            THEN CAST(0 AS BIT)
                        ELSE CAST(1 AS BIT)
                    END AS IsAssigned
                FROM Modules m
                LEFT JOIN CompanyModules cm
                    ON cm.ModuleId = m.Id
                   AND cm.CompanyId = @CompanyId
                ORDER BY m.Name";

            var result =
                await db.QueryAsync<ModuleViewModel>(
                    sql,
                    new { CompanyId = companyId });

            return result.ToList();
        }

        public async Task UpdateModulesAsync(
            Guid companyId,
            List<Guid> moduleIds)
        {
            using var db = _factory.Create();

            await db.ExecuteAsync(
                @"DELETE FROM CompanyModules
                  WHERE CompanyId = @CompanyId",
                new { CompanyId = companyId });

            foreach (var moduleId in moduleIds)
            {
                await db.ExecuteAsync(
                    @"INSERT INTO CompanyModules
                      (
                          CompanyId,
                          ModuleId,
                          IsEnabled
                      )
                      VALUES
                      (
                          @CompanyId,
                          @ModuleId,
                          1
                      )",
                    new
                    {
                        CompanyId = companyId,
                        ModuleId = moduleId
                    });
            }
        }
    }
}