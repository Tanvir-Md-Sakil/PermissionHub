using Dapper;
using PermissionHub.Models;


namespace PermissionHub
{
    public class BudgetRepository
    {
        private readonly IDbConnectionFactory _factory;

        public BudgetRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<BudgetModel>> GetByCompanyAsync(Guid companyId)
        {
            using var db = _factory.Create();

            return (await db.QueryAsync<BudgetModel>(
                "SELECT * FROM Budgets WHERE CompanyId=@companyId",
                new { companyId }
            )).ToList();
        }

        public async Task CreateAsync(BudgetModel b)
        {
            using var db = _factory.Create();

            await db.ExecuteAsync(@"
            INSERT INTO Budgets
            (Id, CompanyId, Title, Amount)
            VALUES
            (@Id, @CompanyId, @Title, @Amount)",
                b);
        }
    }
}
