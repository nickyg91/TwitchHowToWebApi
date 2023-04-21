using Orchard.Data.Entities;

namespace Orchard.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<bool> CreateUser(User user);
    Task<User?> GetUserByEmail(string email);
    Task<bool> SetEmailGuidVerified(int id);
    Task<User?> GetUserByVerificationGuid(Guid guid);
}