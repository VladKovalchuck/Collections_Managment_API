using Collections_Managment_API.Middleware;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Enums;
using CollectionsManagmentAPI.Entity.Extensions;
using Microsoft.AspNetCore.Mvc;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Collections_Managment_API.Controllers;

[ApiController]
public class UserIdentityController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly IUserService _userService;

    public UserIdentityController(IIdentityService identityService, IUserService userService)
    {
        _identityService = identityService;
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserModel>> Register(RegisterModel registerModel)
    {
        var user = await _userService.Create(registerModel);
        
        if(user == null)
            return BadRequest("This username is already in use");
        
        return Ok(user);
    }

    
    [HttpPost("login")]
    public ActionResult<string> Login(LoginModel loginModel)
    {
        var user = _userService.SearchByLogin(loginModel.Login);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (!_identityService.VerifyPasswordHash(loginModel.Password, user.PasswordHash))
        {
            return BadRequest("Wrong password.");
        }

        string token = _identityService.CreateToken(user);
        return Ok(token);
    }
    
}