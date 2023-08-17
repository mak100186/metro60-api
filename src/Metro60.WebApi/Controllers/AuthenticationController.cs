using Metro60.Core.Models;
using Metro60.Core.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Metro60.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
    {
        var user = await _userService.GetUser(model.Username, model.Password);

        if (user == null)
        {
            return BadRequest(new { message = "Username or password is incorrect" });
        }

        return Ok(user);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserModel>))]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();

        return Ok(users);
    }
}
