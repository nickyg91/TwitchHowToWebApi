using Orchard.Models;
using Orchard.Models.Authentication;

namespace Orchard.Services.Domain;

public interface IUserService
{
    Task<UserModel> Authenticate(LoginModel loginCredentials);
    Task<bool> CreateAccount(UserModel user);
    Task<UserModel> GetUserById(int userId);
}