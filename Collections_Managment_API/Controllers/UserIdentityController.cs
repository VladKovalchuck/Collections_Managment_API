using CollectionsManagmentAPI.Entity;
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
    public async Task<ActionResult<UserEntity>> Register(RegisterModel registerModel)
    {
        UserEntity user = await _userService.SearchByLogin(registerModel.Username);
        if (user != null)
        {
            return BadRequest("this username is already in use");
        }
        
        _identityService.CreatePasswordHash(registerModel.Password, out byte[] passwordHash);

        user = new UserEntity()
        {
            PasswordHash = passwordHash,
            Username = registerModel.Username, 
            EmailAddress = registerModel.EmailAddress, 
            FirstName = registerModel.FirstName, 
            LastName = registerModel.LastName
        };

        await _userService.Create(user);
        
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginModel loginModel)
    {
        var user = await _userService.SearchByLogin(loginModel.Login);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        if (!_identityService.VerifyPasswordHash(loginModel.Password, user.PasswordHash))
        {
            return BadRequest("Wrong password.");
        }

        string token = _identityService.CreateToken(user);
        return Ok(token);
    }
    
}