using BEInternProject.Domain.Entities;

namespace BEInternProject.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<int> CreateAsync(User user);
}
