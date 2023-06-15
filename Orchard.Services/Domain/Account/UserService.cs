using AutoMapper;
using Orchard.Core.Exceptions;
using Orchard.Core.Services;
using Orchard.Core.Services.Email;
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
    private readonly IEmailService _emailService;

    public UserService(
        IUserRepository userRepository,
        PasswordHashService passwordHashService, 
        IMapper mapper,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _mapper = mapper;
        _emailService = emailService;
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
        dbUser.HashedPassword = _passwordHashService.HashPassword(user.Password);
        bool userCreated = await _userRepository.CreateUser(dbUser);
        await _emailService.SendAccountVerificationEmail(dbUser.EmailVerificationGuid!.Value, dbUser.Email);
        return userCreated;
    }

    public async Task<UserModel> GetUserById(int userId)
    {
        var dbUser = await _userRepository.GetUserById(userId);
        if (dbUser == null)
        {
            throw new UserNotFoundException("User does not exist.");
        }
        return _mapper.Map<User, UserModel>(dbUser);
    }
}