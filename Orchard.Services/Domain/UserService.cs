using AutoMapper;
using Orchard.Core.Exceptions;
using Orchard.Core.Services;
using Orchard.Data.Entities;
using Orchard.Data.Repositories.Interfaces;
using Orchard.Models;
using Orchard.Models.Authentication;

namespace Orchard.Services.Domain;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHashService _passwordHashService;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, PasswordHashService passwordHashService, IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _mapper = mapper;
    }
        
    public async Task<UserModel> Authenticate(LoginModel loginCredentials)
    {
        User? user = await _userRepository.GetUserByEmail(loginCredentials.Email);
        if (user == null)
        {
            throw new InvalidLoginException("User credentials are invalid!");
        }

        var isPasswordVerified =
            _passwordHashService.IsPasswordVerified(loginCredentials.Password, user.HashedPassword);

        if (!isPasswordVerified)
        {
            throw new InvalidLoginException("User credentials are invalid!");
        }

        return _mapper.Map<User, UserModel>(user);
    }

    public async Task<bool> CreateAccount(UserModel user)
    {
        User dbUser = _mapper.Map<UserModel, User>(user);
        bool userCreated = await _userRepository.CreateUser(dbUser);
        return userCreated;
    }
}