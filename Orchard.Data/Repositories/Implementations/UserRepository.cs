using Microsoft.EntityFrameworkCore;
using Orchard.Data.Contexts;
using Orchard.Data.Entities;
using Orchard.Data.Repositories.Interfaces;

namespace Orchard.Data.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly OrchardDbContext _ctx;
    public UserRepository(OrchardDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<bool> CreateUser(User user)
    {
        _ctx.Users.Add(user);
        var changes = await _ctx.SaveChangesAsync();
        return changes > 0;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _ctx.Users.SingleOrDefaultAsync(x => x.Email == email);
        return user;
    }

    public async Task<bool> SetEmailGuidVerified(int id)
    {
        var user = await _ctx.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }
        user.EmailVerificationGuid = Guid.NewGuid();
        return await _ctx.SaveChangesAsync() > 0;

    }

    public async Task<User?> GetUserByVerificationGuid(Guid guid)
    {
        var user = await _ctx.Users.SingleOrDefaultAsync(x => x.EmailVerificationGuid == guid);
        return user;
    }
}