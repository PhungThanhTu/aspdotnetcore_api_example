using BLL.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Shared
{
    public static class SharedControllerMethods
    {
        public static IActionResult HandleExceptions(BaseCustomApplicationException e, ControllerBase controllerBase)
        {
            if (e is UnauthorizedException)
                return controllerBase.Unauthorized();
            if (e is ResourceNotFoundException)
                return controllerBase.NotFound();
            if (e is ResourceConflictException)
                return controllerBase.BadRequest();
            if (e is NotAllowedException)
                return controllerBase.BadRequest();
            return controllerBase.BadRequest();
        }
    }
}