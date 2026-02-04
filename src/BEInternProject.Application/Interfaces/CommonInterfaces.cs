using System.Data;

namespace BEInternProject.Application.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}

public interface IJwtTokenGenerator
{
    string GenerateToken(int userId, string username, string role);
}
