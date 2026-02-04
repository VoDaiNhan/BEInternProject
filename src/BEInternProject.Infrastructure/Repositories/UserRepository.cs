using BEInternProject.Application.Interfaces;
using BEInternProject.Domain.Entities;
using Dapper;

namespace BEInternProject.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public UserRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<int> CreateAsync(User user)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var sql = @"
            INSERT INTO Users (Username, PasswordHash, Email, Role)
            VALUES (@Username, @PasswordHash, @Email, @Role)
            RETURNING Id;";
        
        return await connection.ExecuteScalarAsync<int>(sql, user);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var sql = "SELECT * FROM Users WHERE Username = @Username";
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
    }
}
