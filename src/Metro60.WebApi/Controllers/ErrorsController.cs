using Metro60.Core.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Metro60.WebApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [AllowAnonymous]
    [Route("error")]
    public ErrorModel Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context.Error;

        var code = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        Response.StatusCode = code; // You can use HttpStatusCode enum instead

        return new ErrorModel(exception);
    }
}
