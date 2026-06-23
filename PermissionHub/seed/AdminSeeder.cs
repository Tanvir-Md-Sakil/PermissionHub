using Dapper;
using global::PermissionHub.Models;
using PermissionHub.Models;


namespace PermissionHub.seed
{

    public class AdminSeeder
    {
        private readonly IDbConnectionFactory _factory;

        public AdminSeeder(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task SeedAsync()
        {
            using var db = _factory.Create();

            // 1. Check if admin exists
            var exists = await db.ExecuteScalarAsync<int>(@"
            SELECT COUNT(*)
            FROM Users
            WHERE Email = @Email",
                new { Email = "admin@test.com" });

            if (exists > 0)
                return; // already seeded

            // 2. Create admin user
            var admin = new User
            {
                Id = Guid.NewGuid(),
                FullName = "System Admin",
                Email = "admin@test.com",
                PasswordHash = PasswordHelper.Hash("123456")
            };

            await db.ExecuteAsync(@"
            INSERT INTO Users (Id, FullName, Email, PasswordHash)
            VALUES (@Id, @FullName, @Email, @PasswordHash)",
                admin);
        }
    }
}
