using Dapper;
using global::PermissionHub.Models;
using PermissionHub.Models;


namespace PermissionHub
{
    public class CompanyRepository
    {
        private readonly IDbConnectionFactory _factory;

        public CompanyRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            using var db = _factory.Create();

            var sql = @"
            SELECT Id, Name, IsActive
            FROM Companies
            ORDER BY Name";

            var result = await db.QueryAsync<Company>(sql);

            return result.ToList();
        }

        public async Task<Company?> GetByIdAsync(Guid id)
        {
            using var db = _factory.Create();

            var sql = @"
            SELECT Id, Name, IsActive
            FROM Companies
            WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Company>(
                sql,
                new { Id = id });
        }

        public async Task<Guid> CreateAsync(Company company)
        {
            using var db = _factory.Create();

            company.Id = Guid.NewGuid();

            var sql = @"
            INSERT INTO Companies (Id, Name, IsActive)
            VALUES (@Id, @Name, @IsActive)";

            await db.ExecuteAsync(sql, company);

            return company.Id;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            using var db = _factory.Create();

            var sql = @"
            SELECT COUNT(1)
            FROM Companies
            WHERE Name = @Name";

            var count = await db.ExecuteScalarAsync<int>(sql, new { Name = name });

            return count > 0;
        }
    }
}
