using BLL.Exceptions;
using System.Security.Claims;

namespace BLL.Common;

public static class SharedLibrary
{
    public static Guid? GetUserIdFromAuthorization(ClaimsPrincipal user)
    {
        var claimsIdentity = user.Identity as ClaimsIdentity;
        var claim = claimsIdentity?.FindFirst(ClaimTypes.Sid);

        var userId = claim?.Value;
        if (userId is null) throw new UnauthorizedException();
        return new Guid(userId);
    }
}