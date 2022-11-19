using AutoMapper;
using BLL.Common;
using BLL.DTOs.Users;
using BLL.Exceptions;
using DAL.Aggregates;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services;

public class UserService : IUserServices
{
    private readonly ISharedRepositories _sharedRepositories;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IGenericRepository<User> _userRepository;

    public UserService(ISharedRepositories sharedRepositories, IMapper mapper, IConfiguration configuration)
    {
        _sharedRepositories = sharedRepositories;
        _mapper = mapper;
        _configuration = configuration;
        _userRepository = _sharedRepositories.RepositoriesManager.UserRepository;
    }



    public Guid Register(RegisterRequest request)
    {
        var foundUser = _userRepository.Get(user => user.Username == request.Username, null, 1);
        if (foundUser.Any())
        {
            throw new InvalidOperationException($"Username {request.Username} has existed in the current context");
        }

        User newUser = _mapper.Map<User>(request);
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        _userRepository.Insert(newUser);
        _sharedRepositories.RepositoriesManager.Saves();
        return newUser.Id;
    }
    public AuthenticationResponse Authenticate(LoginRequest loginRequest)
    {
        if (loginRequest.Username is null || loginRequest.RawPassword is null) throw new UnauthorizedException();
        User? user = GetUserByUsername(loginRequest.Username);

        if (user is null) throw new UnauthorizedException();

        if(user.Password is not null && !Verify(loginRequest.RawPassword,user.Password))
        {
            throw new UnauthorizedException();
        }

        string? accessToken = GenerateJWT(user);

        return new AuthenticationResponse
        {
            Token = accessToken
        };
    }

    private string? GenerateJWT(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);
        if (user.Username == null) return null;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
     
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:Expires"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private User? GetUserByUsername(string username)
    {
        if (!_userRepository.Get(user => user.Username == username, null, 1).Any()) return null;
        return _userRepository.Get(user => user.Username == username, null, 1).First();
    }
    
    private static bool Verify(string rawPasword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(rawPasword, hashedPassword);
    }


}