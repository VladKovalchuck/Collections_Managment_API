using Collections_Managment_API.Middleware;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Enums;
using CollectionsManagmentAPI.Entity.Extensions;
using Microsoft.AspNetCore.Mvc;
using CollectionsManagmentAPI.Service.Interfaces;

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
        var user = _userService.SearchByLogin(registerModel.Username);
        if (user != null)
        {
            return BadRequest("This username is already in use");
        }
        
        _identityService.CreatePasswordHash(registerModel.Password, out byte[] passwordHash);

        user = new UserEntity()
        {
            PasswordHash = passwordHash,
            Username = registerModel.Username, 
            EmailAddress = registerModel.EmailAddress, 
            Role = (int)Roles.User,
            FirstName = registerModel.FirstName, 
            LastName = registerModel.LastName
        };

        await _userService.Create(user);
        
        return Ok(user.ConvertToUserModel());
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