using Dapper;
using PermissionHub.Models;

namespace PermissionHub
{

    public class UserRepository
    {
        private readonly IDbConnectionFactory _factory;

        public UserRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<User?> GetByEmail(string email)
        {
            using var db = _factory.Create();

            return await db.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE Email=@Email",
                new { Email = email });
        }

        public async Task CreateAsync(User user)
        {
            using var db = _factory.Create();

            await db.ExecuteAsync(@"
        INSERT INTO Users(Id,FullName,Email,PasswordHash)
        VALUES(@Id,@FullName,@Email,@PasswordHash)", user);
        }

        public async Task<List<User>> GetAll()
        {
            using var db = _factory.Create();

            var sql = "SELECT * FROM Users";

            return (await db.QueryAsync<User>(sql)).ToList();
        }
    }
}
