using BLL.DTOs.Users;
using DAL.Aggregates;
using System.Security.Claims;

namespace BLL.Services;

public interface IUserServices
{
    Guid Register(RegisterRequest request);

    AuthenticationResponse Authenticate(LoginRequest loginRequest);



}